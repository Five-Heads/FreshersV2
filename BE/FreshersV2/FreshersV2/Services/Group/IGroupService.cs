using FreshersV2.Models.Group.Create;

namespace FreshersV2.Services.Group
{
    public interface IGroupService
    {
        Task CreateGroup(CreateGroupRequestModel group);

        Task DeleteGroup(int id);
    }
}
