using FreshersV2.Hubs;
using FreshersV2.Services.ImageVote;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FreshersV2.Jobs
{
    public class VoteRoundJob
    {
        private readonly IHubContext<VoteImageHub> context;
        private readonly string contestId;
        private readonly IImageVoteService service;
        private readonly Random random;
        private readonly int voteTimeExtraDelay = 1000;

        private int currentRoundId;

        public VoteRoundJob(IImageVoteService service, IHubContext<VoteImageHub> context, string contestId)
        {
            this.context = context;
            this.contestId = contestId;
            this.service = service;

            this.random = new Random();
            this.currentRoundId = 0;
        }

        public async Task Execute()
        {
            // TODO: admin send req to start the contest and set DU to ALL
            while (true)
            {
                var roundId = await this.service.GetNextRoundId(this.contestId);
                currentRoundId = roundId;
                var drawingUsers = await this.service.GetDrawingUsers(this.contestId);
                foreach (var user in drawingUsers)
                {
                    await this.context.Clients.User(user.UserHubId).SendAsync("StartRound",contestId, roundId, true);
                }

                await this.context.Clients.Group(this.contestId).SendAsync("StartRound", roundId);

                var contest = await this.service.GetContest(contestId);
                var images = await service.GetRoundImages(contestId, roundId);
                var shuffledImages = images.OrderBy(_ => random.Next()).ToList();

                for (int i = 0; i < shuffledImages.Count; i += 2)
                {
                    var image1 = shuffledImages[i];
                    var image2 = shuffledImages[i + 1];
                    await this.context.Clients.Group(contestId).SendAsync("StartRound", image1, image2);
                    await Task.Delay((contest.VoteTime * 1000) + voteTimeExtraDelay);
                }

                await this.context.Clients.Group(contestId).SendAsync("EndRound");
            }
        }
    }
}