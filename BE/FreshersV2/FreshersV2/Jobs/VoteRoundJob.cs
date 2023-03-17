using FreshersV2.Hubs;
using FreshersV2.Services.ImageVote;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Jobs
{
    public class VoteRoundJob
    {
        private readonly IHubContext<VoteImageHub> context;
        private readonly IImageVoteService service;
        private readonly Random random;
        private readonly int waitTimeExtraDelay = 1000;
        private readonly int viewResultsTimeDelay = 5000;

        private int currentRoundId;
        private int contestId;

        public VoteRoundJob(IImageVoteService service, IHubContext<VoteImageHub> context)
        {
            this.context = context;
            this.service = service;

            this.random = new Random();
            this.currentRoundId = 0;
        }

        public async Task Execute(int contestId)
        {
            var contest = await this.service.GetContest(contestId);
            var participant = contest.UserContests;
            var words = contest.Words.Split(',', StringSplitOptions.RemoveEmptyEntries).OrderBy(_ => random.Next()).ToList();
            var drawingUsersHubIds = (await this.service.GetInitialDrawingUsers(this.contestId, currentRoundId)).Select(x => x.UserHubId).ToList();


            while (drawingUsersHubIds.Count > 0)
            {
                var word = words[currentRoundId];
                currentRoundId++;

                await service.CreateRound(contestId, currentRoundId, word, drawingUsersHubIds);

                foreach (var user in participant)
                {
                    bool isDrawing = drawingUsersHubIds.Contains(user.UserHubId);
                    await this.context.Clients.User(user.UserHubId).SendAsync("StartRound", contestId, currentRoundId, word, word, contest.DrawTime, isDrawing);
                }

                await Task.Delay((contest.DrawTime * 1000) + waitTimeExtraDelay);

                var images = await service.GetRoundImages(contestId, currentRoundId);
                var shuffledImages = images.OrderBy(_ => random.Next()).ToList();
                drawingUsersHubIds.Clear();

                for (int i = 0; i < shuffledImages.Count; i += 2)
                {
                    var image1 = shuffledImages[i];
                    var image2 = shuffledImages[i + 1];
                    var voteRoundId = await service.CreateVoteRound(contestId, currentRoundId, image1, image2);
                    await this.context.Clients.Group(contestId.ToString()).SendAsync("StartVote", image1, image2, word, contest.VoteTime);
                    await Task.Delay((contest.VoteTime * 1000) + waitTimeExtraDelay);

                    var voteRound = await service.GetRoundVote(voteRoundId);
                    if (voteRound.Image1Votes == voteRound.Image2Votes)
                    {
                        drawingUsersHubIds.Add(image1.User.UserHubId);
                        drawingUsersHubIds.Add(image2.User.UserHubId);
                    }
                    else if (voteRound.Image1Votes > voteRound.Image2Votes)
                    {
                        drawingUsersHubIds.Add(image1.User.UserHubId);
                    }
                    else
                    {
                        drawingUsersHubIds.Add(image2.User.UserHubId);
                    }
                }

                if (shuffledImages.Count % 2 == 1)
                {
                    drawingUsersHubIds.Add(shuffledImages.Last().User.UserHubId);
                }

                await this.context.Clients.Group(contestId.ToString()).SendAsync("EndRound", "LEADERBOARD");

                await Task.Delay(viewResultsTimeDelay + waitTimeExtraDelay);
            }

            await context.Clients.Groups(contestId.ToString()).SendAsync("Finish", "LEADERBOARD");
        }
    }
}