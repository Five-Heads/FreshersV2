using FreshersV2.Data.Models.BlurredImageGame;
using FreshersV2.Models.BlurredImage;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BaseImageContest;
using FreshersV2.Services.BlurredImage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlurredImageContestController : BaseApiController
    {
        private readonly IBlurredImageContestService blurredImageContestService;

        public BlurredImageContestController(IBlurredImageContestService blurredImageContestService)
        {
            this.blurredImageContestService = blurredImageContestService;
        }

        [HttpPost("create")]
        public async Task CreateContest(CreateBlurredImageContestRequestModel model)
        {
            await this.blurredImageContestService.CreateContest(model);
        }

        [HttpGet("upcoming")]
        public async Task<BlurredImageContest?> GetUpcomingContest()
        {
           return await this.blurredImageContestService.GetUpcomingContest();
        }

        [HttpPost("add-user-to-contest")]
        public async Task AddUserToContest(AddUserToContestRequestModel model)
        {
           await this.blurredImageContestService.AddUserToUpcomingContest(model.UserId);
        }

        [HttpGet("contest-users")]
        public async Task<List<string>> GetUpcomingContestUsers()
        {
            return await this.blurredImageContestService.GetUpcomingContestUsers();
        }

        [HttpPost("user-rankings")]
        public async Task AddUserPoints(BlurredImageContestResultsRequestModel model)
        {
            var userId=this.GetUserId();
            await this.blurredImageContestService.AddUsersPointsToLeaderboard(model.Round,userId);
        }

        [HttpPost("change-status")]
        public async Task AddUserPoints(ChangeStatusRequestModel model)
        {
            await this.blurredImageContestService.ChangeStatus(model.Status);
        }
    }
}
