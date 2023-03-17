using FreshersV2.Models.Authentication;

namespace FreshersV2.Services.Identity
{
    public interface IIdentityService
    {
        Task<Data.Models.User> Register(RegisterRequestModel model);

        Task<Data.Models.User> Login(LoginRequestModel model);

        string GenerateJwtToken(string userId, string userName, string role, string secret);
    }
}
