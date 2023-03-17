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
            if (!await identityService.Register(model))
            {
                return this.Unauthorized();
            }

            return new AuthResponseModel
            {
                Token = identityService.GenerateJwtToken(model.UserName, model.UserName, this.applicationSettings.Secret)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseModel>> Login([FromBody] LoginRequestModel model)
        {
            if (!await this.identityService.Login(model))
            {
                return this.Unauthorized();
            }

            return new AuthResponseModel
            {
                Token = identityService.GenerateJwtToken(model.UserName, model.UserName, this.applicationSettings.Secret)
            };
        }
    }
}
