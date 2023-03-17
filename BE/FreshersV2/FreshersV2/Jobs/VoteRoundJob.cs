using FreshersV2.Services.ImageVote;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Jobs
{
    public class VoteRoundJob
    {
        private readonly IHubContext context;
        private readonly string contestId;
        private readonly int roundId;
        private readonly IImageVoteService service;
        private readonly Random random;

        private readonly int voteTimeExtraDelay = 1000;

        public VoteRoundJob(IImageVoteService service, IHubContext context, string contestId, int roundId)
        {
            this.context = context;
            this.contestId = contestId;
            this.roundId = roundId;
            this.service = service;
            this.random = new Random();
        }

        public async Task Execute()
        {
            
            var contest = await this.service.GetContest(contestId);
            var images = await service.GetRoundImages(contestId,roundId);
            var shuffulledImages = images.OrderBy(_ => random.Next()).ToList();

            for (int i = 0; i < shuffulledImages.Count; i += 2)
            {
                var image1 = shuffulledImages[i];
                var image2 = shuffulledImages[i + 1];
                await this.context.Clients.Group(contestId).SendAsync("StartRound", image1, image2);
                await Task.Delay((contest.VoteTime * 1000) + voteTimeExtraDelay);
            }

            await this.context.Clients.Group(contestId).SendAsync("EndRound");
        }
    }
}