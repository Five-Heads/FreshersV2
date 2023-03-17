using FreshersV2.Models.Authentication;

namespace FreshersV2.Services.Identity
{
    public interface IIdentityService
    {
        Task<bool> Register(RegisterRequestModel model);

        Task<bool> Login(LoginRequestModel model);

        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
