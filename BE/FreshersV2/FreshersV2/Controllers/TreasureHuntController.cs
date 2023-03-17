using FreshersV2.Services.TreasureHunt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreshersV2.Controllers
{
    [Authorize]
    public class TreasureHuntController : BaseApiController
    {
        private readonly ITreasureHuntService treasureHuntService;

        public TreasureHuntController(ITreasureHuntService treasureHuntService)
        {
            this.treasureHuntService = treasureHuntService;
        }

        [HttpGet]
        public async Task My()
        {
            var userId = this.ExtractClaim<int>(ClaimTypes.NameIdentifier);
            

        }
    }
}
