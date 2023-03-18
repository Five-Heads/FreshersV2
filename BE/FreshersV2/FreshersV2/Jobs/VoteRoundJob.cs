using FreshersV2.Hubs;
using FreshersV2.Services.ImageVote;
using FreshersV2.Services.Leaderboard;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Jobs
{
    public class VoteRoundJob
    {
        private readonly IHubContext<VoteImageHub> context;
        private readonly IImageVoteService imageService;
        private readonly ILeaderboardService leaderboardService;

        private readonly int waitTimeExtraDelay = 1000;
        private readonly int viewResultsTimeDelay = 5000;
        private readonly int scoreForWin = 10;

        private readonly Random random;
        private int currentRoundId;
        private int contestId;

        public VoteRoundJob(IImageVoteService imageService, ILeaderboardService leaderboardService, IHubContext<VoteImageHub> context)
        {
            this.context = context;
            this.imageService = imageService;
            this.leaderboardService = leaderboardService;

            this.random = new Random();
            this.currentRoundId = 0;
        }

        public async Task Execute(int contestId)
        {
            var contest = await this.imageService.GetContest(contestId);
            var participant = contest.UserContests;
            var words = contest.Words.Split(',', StringSplitOptions.RemoveEmptyEntries).OrderBy(_ => random.Next()).ToList();
            var drawingUsersHubIds = (await this.imageService.GetInitialDrawingUsers(this.contestId, currentRoundId)).Select(x => x.UserHubId).ToList();

            Dictionary<string, int> scores = new Dictionary<string, int>();

            while (drawingUsersHubIds.Count > 0)
            {
                var word = words[currentRoundId];
                currentRoundId++;

                await imageService.CreateRound(contestId, currentRoundId, word, drawingUsersHubIds);

                foreach (var user in participant)
                {
                    bool isDrawing = drawingUsersHubIds.Contains(user.UserHubId);
                    await this.context.Clients.User(user.UserHubId).SendAsync("StartRound", contestId, currentRoundId, word, contest.DrawTime, isDrawing);
                }

                await Task.Delay((contest.DrawTime * 1000) + waitTimeExtraDelay);

                var images = await imageService.GetRoundImages(contestId, currentRoundId);
                var shuffledImages = images.OrderBy(_ => random.Next()).ToList();
                drawingUsersHubIds.Clear();

                for (int i = 0; i < shuffledImages.Count; i += 2)
                {
                    var image1 = shuffledImages[i];
                    var image2 = shuffledImages[i + 1];
                    var voteRoundId = await imageService.CreateVoteRound(contestId, currentRoundId, image1, image2);
                    await this.context.Clients.Group(contestId.ToString()).SendAsync("StartVote", image1, image2, word, contest.VoteTime);
                    await Task.Delay((contest.VoteTime * 1000) + waitTimeExtraDelay);

                    var voteRound = await imageService.GetRoundVote(voteRoundId);
                    if (voteRound.Image1Votes == voteRound.Image2Votes)
                    {
                        drawingUsersHubIds.Add(image1.User.UserHubId);
                        drawingUsersHubIds.Add(image2.User.UserHubId);
                    }
                    else if (voteRound.Image1Votes > voteRound.Image2Votes)
                    {
                        drawingUsersHubIds.Add(image1.User.UserHubId);
                        if (!scores.ContainsKey(image1.User.UserHubId))
                        {
                            scores.Add(image1.User.User.Name, 0);
                        }

                        scores[image1.User.User.Name] += scoreForWin;
                    }
                    else
                    {
                        drawingUsersHubIds.Add(image2.User.UserHubId);
                        if (!scores.ContainsKey(image1.User.UserHubId))
                        {
                            scores.Add(image2.User.User.Name, 0);
                        }
                        scores[image2.User.User.Name] += scoreForWin;
                    }
                }

                if (shuffledImages.Count % 2 == 1)
                {
                    drawingUsersHubIds.Add(shuffledImages.Last().User.UserHubId);
                }

                await this.context.Clients.Group(contestId.ToString()).SendAsync("EndRound", scores.OrderBy(x => x.Value).ToList());

                await Task.Delay(viewResultsTimeDelay + waitTimeExtraDelay);
            }

            foreach (var score in scores)
            {
                await leaderboardService.AddPoints(score.Key, score.Value);
            }

            await context.Clients.Groups(contestId.ToString()).SendAsync("Finish", scores.OrderBy(x => x.Value).ToList());
        }
    }
}