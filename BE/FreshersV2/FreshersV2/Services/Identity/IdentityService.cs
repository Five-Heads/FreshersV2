using FreshersV2.Data.Models;
using FreshersV2.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreshersV2.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Data.Models.User> userManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityService(UserManager<Data.Models.User> userManager, IOptions<ApplicationSettings> applicationSettings)
        {
            this.userManager = userManager;
            this.applicationSettings = applicationSettings.Value;
        }

        public async Task<Data.Models.User> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null || !await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<Data.Models.User> Register(RegisterRequestModel model)
        {
            var user = new Data.Models.User
            {
                UserName = model.UserName,
                FacultyNumber = model.FacultyNumber
            };

            return (await userManager.CreateAsync(user, model.Password)).Succeeded ? user : null;
        }

        public string GenerateJwtToken(string userId, string userName, string role, string secret)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
