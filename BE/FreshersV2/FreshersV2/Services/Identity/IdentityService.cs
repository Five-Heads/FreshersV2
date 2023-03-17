using FreshersV2.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreshersV2.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Data.Models.User> userManager;

        public IdentityService(UserManager<Data.Models.User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return false;
            }

            return await this.userManager.CheckPasswordAsync(user, model.Password);
        }

        public async Task<bool> Register(RegisterRequestModel model)
        {
            var user = new Data.Models.User
            {
                UserName = model.UserName,
                Name = "",
                FacultyNumber = "" // TODO: fix
            };

            var identityResult = await userManager.CreateAsync(user, model.Password);
            return identityResult.Succeeded;
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
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
