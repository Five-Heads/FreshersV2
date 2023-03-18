﻿using FreshersV2.Data.Models.BlurredImageGame;
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
        public async Task AddUserPoints(List<BlurredImageContestResultsRequestModel> results)
        {
            await this.blurredImageContestService.AddUsersPointsToLeaderboard(results);
        }
    }
}
