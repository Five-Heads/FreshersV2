using FreshersV2.Services.TreasureHunt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreshersV2.Controllers
{
    //[Authorize]
    public class TreasureHuntController : BaseApiController
    {
        private readonly ITreasureHuntService treasureHuntService;

        public TreasureHuntController(ITreasureHuntService treasureHuntService)
        {
            this.treasureHuntService = treasureHuntService;
        }

        [HttpGet("my")]
        public async Task My()
        {
            var userId = this.ExtractClaim<string>(ClaimTypes.NameIdentifier);
            var a = 5;

        }
    }
}
