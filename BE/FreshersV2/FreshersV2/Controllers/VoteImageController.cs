using FreshersV2.Data.Models.VoteImageGame;
using FreshersV2.Jobs;
using FreshersV2.Services.ImageVote;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{

   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VoteImageController : BaseApiController
    {
        private readonly IImageVoteService imageVoteService;
        private readonly VoteRoundJob voteRoundJob;

        public VoteImageController(IImageVoteService imageVoteService, VoteRoundJob voteRoundJob)
        {
            this.imageVoteService = imageVoteService;
            this.voteRoundJob = voteRoundJob;
        }

        [HttpPost("createContest")]
        public async Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime, List<string> words)
        {
            await imageVoteService.CreateContest(name, maxParticipants, voteTime, drawTime, words);
        }

        [HttpGet("all")]
        public object All()
        {
            return new { contest = imageVoteService.AllContests() };
        }

        [HttpPost("startContest")]
        public void StartContest(int id)
        {
            if (IsInRole("admin"))
            {
                BackgroundJob.Enqueue(() => voteRoundJob.Execute(id));
            }
        }


    }
}
