using FreshersV2.Data.Models.VoteImageGame;
using FreshersV2.Hubs;
using FreshersV2.Jobs;
using FreshersV2.Models.VoteImage;
using FreshersV2.Services.ImageVote;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task CreateContest([FromBody] CreateContestModel model)
        {
            await imageVoteService.CreateContest(model.Name, model.MaxParticipants, model.VoteTime, model.DrawTime, model.Words);
        }

        [HttpGet("all")]
        public object All()
        {
            return new
            {
                contest = imageVoteService.AllContests()
            };
        }

        [HttpPost("startContest")]
        public void StartContest([FromBody] StartContestModel model)
        {
            // if (IsInRole("admin"))
            {
                BackgroundJob.Enqueue(() => voteRoundJob.Execute(model.Id));
            }
        }


    }
}
