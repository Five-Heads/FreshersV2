using FreshersV2.Models.BlurredImage;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BaseImageContest;
using FreshersV2.Services.BlurredImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task GetUpcomingContest()
        {
            await this.blurredImageContestService.GetUpcomingContest();
        }

        [HttpGet("add-user-to-contest")]
        public async Task AddUserToContest()
        {
            await this.blurredImageContestService.GetUpcomingContest();
        }
    }
}
