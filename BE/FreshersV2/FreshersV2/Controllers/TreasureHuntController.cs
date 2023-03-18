using FreshersV2.Data.Models;
using FreshersV2.Hubs;
using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.Start;
using FreshersV2.Models.TreasureHunt.Validate;
using FreshersV2.Services.TreasureHunt;
using FreshersV2.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TreasureHuntController : BaseApiController
    {
        private readonly ITreasureHuntService treasureHuntService;
        private readonly IHubContext<TreasureHuntHub> treasureHuntHubContext;
        private readonly IUserService userService;

        public TreasureHuntController(
            ITreasureHuntService treasureHuntService,
            IHubContext<TreasureHuntHub> treasureHuntHubContext,
            IUserService userService
            )
        {
            this.treasureHuntService = treasureHuntService;
            this.treasureHuntHubContext = treasureHuntHubContext;
            this.userService = userService;
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


        [HttpPost("start/{id}")]
        public async Task<StartTreasureHuntResponseModel> Start([FromRoute] int id)
        {
            var userId = this.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            
            return await this.treasureHuntService.StartTreasureHunt(id, userId);
        }

        [HttpPost("validate")]
        public async Task<bool> Validate([FromBody] ValidateCheckpointRequestModel model)
        {
            var userId = this.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var result = await this.treasureHuntService.ValidateNextCheckpointForUser(model.CheckpointId, model.TreasureHuntId, userId);

            if (!result) 
            {
                return false;
            }

            // update the next for user
            await this.treasureHuntService.UpdateNextCheckpointForUser(model.TreasureHuntId, userId);

            var groupId = await this.userService.GetUserGroup(userId);

            // notify all other
            await this.treasureHuntHubContext.Clients.Group(groupId.ToString()).SendAsync("CheckpointReached", userId);

            if (groupId != 0)
            {
                // if all have reached send next info
                var newNext = await this.treasureHuntService.CheckIfAllHaveReachedCheckpoint(groupId, model.TreasureHuntId);

                if (newNext != null)
                {
                    await this.treasureHuntHubContext.Clients.Group(groupId.ToString()).SendAsync("NextCheckpoint", newNext);
                }
            }

            return true;
        }
    }
}
