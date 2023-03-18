using FreshersV2.Data.Models;
using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.Start;
using FreshersV2.Services.TreasureHunt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("create")]
        public async Task Create([FromBody] CreateTreasureHuntRequestModel model)
        {
            await this.treasureHuntService.CreateTreasureHunt(model);
        }

        [HttpGet("my")]
        public async Task<List<TreasureHunt>> My()
        {
            var userId = this.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return new List<TreasureHunt>();
            }

            return await this.treasureHuntService.GetUserTreasureHunts(userId);
        }


        [HttpPost("Start/{id}")]
        public async Task<StartTreasureHuntResponseModel> Start([FromRoute] int id)
        {
            var userId = this.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            
            return await this.treasureHuntService.StartTreasureHunt(id, userId);
        }
    }
}
