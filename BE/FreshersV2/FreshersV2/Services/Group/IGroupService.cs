using FreshersV2.Models.Group;
using FreshersV2.Models.Group.Create;

namespace FreshersV2.Services.Group
{
    public interface IGroupService
    {
        Task<GroupInfoResponseModel> GetUserGroup(string userId);

        Task<int> CreateGroup(CreateGroupRequestModel group);

        Task DeleteGroup(int id);
    }
}
