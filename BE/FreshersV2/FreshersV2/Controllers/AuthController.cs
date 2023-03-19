using FreshersV2.Data.Models;
using FreshersV2.Models.Authentication;
using FreshersV2.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FreshersV2.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IIdentityService identityService;
        private readonly ApplicationSettings applicationSettings;

        public AuthController(
            IIdentityService identityService,
            IOptions<ApplicationSettings> applicationSettings)
        {
            this.identityService = identityService;
            this.applicationSettings = applicationSettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseModel>> Register([FromBody] RegisterRequestModel model)
        {
            var registerUser = await identityService.Register(model);
            if (registerUser == null)
            {
                return this.BadRequest();
            }

            return new AuthResponseModel
            {
                Id = registerUser.Id,
                UserName = registerUser.UserName,
                FacultyNumber = registerUser.FacultyNumber,
                Token = identityService.GenerateJwtToken(registerUser.Id, registerUser.UserName, Enum.GetName(typeof(Role), registerUser.Role), this.applicationSettings.Secret)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseModel>> Login([FromBody] LoginRequestModel model)
        {
            var registerUser = await this.identityService.Login(model);
            if (registerUser == null)
            {
                return this.Unauthorized();
            }

            return new AuthResponseModel
            {
                Id = registerUser.Id,
                UserName = registerUser.UserName,
                FacultyNumber = registerUser.FacultyNumber,
                Token = identityService.GenerateJwtToken(registerUser.Id, registerUser.UserName, Enum.GetName(typeof(Role), registerUser.Role), this.applicationSettings.Secret)
            };
        }
    }
}
