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
        public async Task My()
        {
            var role = this.ExtractClaim<string>(ClaimTypes.Role);
            if (role != Enum.GetName(typeof(Role), Role.Admin))
            {
                return;
            }

            var userId = this.ExtractClaim<string>(ClaimTypes.NameIdentifier);
            var a = 5;

        }
    }
}
