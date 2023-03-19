using FreshersV2.Data.Models;
using FreshersV2.Models.Leaderboard;
using FreshersV2.Services.Leaderboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LeaderboardController : BaseApiController
    {
        private readonly ILeaderboardService leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            this.leaderboardService = leaderboardService;
        }

        [HttpGet("all")]
        public async Task<List<LeaderboardEntityResponseModel>> All()
        {
            return await leaderboardService.All();
        }
    }
}
