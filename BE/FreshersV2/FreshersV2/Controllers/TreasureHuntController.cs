using FreshersV2.Data.Models;
using FreshersV2.Hubs;
using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.Start;
using FreshersV2.Models.TreasureHunt.Validate;
using FreshersV2.Services.TreasureHunt;
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

        public TreasureHuntController(
            ITreasureHuntService treasureHuntService,
            IHubContext<TreasureHuntHub> treasureHuntHubContext
            )
        {
            this.treasureHuntService = treasureHuntService;
            this.treasureHuntHubContext = treasureHuntHubContext;
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

            // notify all other
            await this.treasureHuntHubContext.Clients.Group(model.GroupId.ToString()).SendAsync("CheckpointReached", userId);

            // if all have reached send next info
            await this.treasureHuntService.CheckIfAllHaveReachedCheckpoint(model.GroupId, model.CheckpointId);
            await this.treasureHuntHubContext.Clients.Group(model.GroupId.ToString()).SendAsync("NextCheckpoint", null);

            return true;
        }
    }
}
