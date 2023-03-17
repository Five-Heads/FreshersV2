using FreshersV2.Models.Authentication;
using FreshersV2.Models.Group.Create;
using FreshersV2.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class GroupsController : ControllerBase
    {
        [HttpPost("create")]
        public void Create([FromBody] CreateGroupRequestModel model)
        {

        }

        [HttpDelete("delete/{id}")]
        public void Delete([FromRoute] int id)
        {

        }
    }
}
