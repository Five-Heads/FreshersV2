using FreshersV2.Models;

namespace FreshersV2.Services.User
{
    public interface IUserService
    {
        Task<List<UserResponseModel>> GetUsersWithoutGroup();

        Task<int> GetUserGroup(string userId);
    }
}
