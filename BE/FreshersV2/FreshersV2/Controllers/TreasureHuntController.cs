using FreshersV2.Data.Models;
using FreshersV2.Services.TreasureHunt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreshersV2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TreasureHuntController : BaseApiController
    {
        private readonly ITreasureHuntService treasureHuntService;

        public TreasureHuntController(ITreasureHuntService treasureHuntService)
        {
            this.treasureHuntService = treasureHuntService;
        }

        [HttpGet("my")]
        public async Task<List<TreasureHunt>> My()
        {
            var userId = this.ExtractClaim<string>(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return new List<TreasureHunt>();
            }

            return await this.treasureHuntService.GetUserTreasureHunts(userId);
        }

        [HttpPost("Start/{id}")]
        public async Task Start([FromRoute]int id)
        {

        }

        [HttpPost("Continue/{id}")]
        public async Task Continue([FromRoute] int id)
        {

        }
    }
}
