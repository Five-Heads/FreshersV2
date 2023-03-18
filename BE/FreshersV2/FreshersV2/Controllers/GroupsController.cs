using FreshersV2.Models.Authentication;
using FreshersV2.Models.Group;
using FreshersV2.Models.Group.Create;
using FreshersV2.Services.Group;
using FreshersV2.Services.Identity;
using FreshersV2.Services.TreasureHunt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupsController : BaseApiController
    {
        private readonly IGroupService groupService;
        private readonly ITreasureHuntService treasureHuntService;

        public GroupsController(IGroupService groupService, ITreasureHuntService treasureHuntService)
        {
            this.groupService = groupService;
            this.treasureHuntService = treasureHuntService;
        }

        [HttpGet("my")]
        public async Task<GroupInfoResponseModel> My()
        {
            var userId = this.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return await this.groupService.GetUserGroup(userId);
        }

        [HttpPost("create")]
        public async Task Create([FromBody] CreateGroupRequestModel model)
        {
            var groupId = await this.groupService.CreateGroup(model);
            if (groupId != 0)
            {
                await this.treasureHuntService.AssignGroupToTreasureHunt(model.TreasureHuntId, groupId);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await this.groupService.DeleteGroup(id);
        }
    }
}
