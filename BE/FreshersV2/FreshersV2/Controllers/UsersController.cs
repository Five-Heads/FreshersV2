using FreshersV2.Data.Models;
using FreshersV2.Models;
using FreshersV2.Services.TreasureHunt;
using FreshersV2.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshersV2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("withoutGroup")]
        public async Task<List<UserResponseModel>> WithoutGroup()
        {
            return await this.userService.GetUsersWithoutGroup();
        }
    }
}
