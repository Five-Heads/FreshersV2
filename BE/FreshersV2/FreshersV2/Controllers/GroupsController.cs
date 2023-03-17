using FreshersV2.Models.Authentication;
using FreshersV2.Models.Group.Create;
using FreshersV2.Services.Group;
using FreshersV2.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : BaseApiController
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpPost("create")]
        public async Task Create([FromBody] CreateGroupRequestModel model)
        {
            await this.groupService.CreateGroup(model);
        }

        [HttpDelete("delete/{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await this.groupService.DeleteGroup(id);
        }
    }
}
