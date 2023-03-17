using FreshersV2.Data.Models.VoteImageGame;
using FreshersV2.Jobs;
using FreshersV2.Services.ImageVote;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    //  [Authorize]
    public class VoteImageController : BaseApiController
    {
        private readonly IImageVoteService imageVoteService;
        private readonly VoteRoundJob voteRoundJob;

        public VoteImageController(IImageVoteService imageVoteService, VoteRoundJob voteRoundJob)
        {
            this.imageVoteService = imageVoteService;
            this.voteRoundJob = voteRoundJob;
        }

        [HttpPost]
        public async Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime, List<string> words)
        {
            await imageVoteService.CreateContest(name, maxParticipants, voteTime, drawTime, words);
        }

        [HttpGet]
        public object All()
        {
            return new { contest = imageVoteService.AllContests() };
        }

        [HttpPost]
        public void StartContest(int id)
        {
            if (IsInRole("admin"))
            {
                BackgroundJob.Enqueue(() => voteRoundJob.Execute(id));
            }
        }


    }
}
