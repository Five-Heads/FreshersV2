using FreshersV2.Data;
using FreshersV2.Models.Group;
using FreshersV2.Models.Group.Create;
using Microsoft.EntityFrameworkCore;

namespace FreshersV2.Services.Group
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext appDbContext;

        public GroupService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<GroupInfoResponseModel> GetUserGroup(string userId)
        {
            return await this.appDbContext
                .Users
                .Where(x => x.GroupId.HasValue && x.Id == userId)
                .Include(x => x.Group)
                .Select(x => new GroupInfoResponseModel
                {
                    Id = x.GroupId.Value,
                    Name = x.Group.Name,
                    Users = x.Group.Users.Select(u => new Models.UserResponseModel
                    {
                        Id = u.Id,
                        FacultyNumber = u.FacultyNumber,
                        UserName = u.UserName
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateGroup(CreateGroupRequestModel group)
        {
            var toSave = new Data.Models.Group
            {
                Name = group.Name
            };
            await this.appDbContext.Groups.AddAsync(toSave);
            await this.appDbContext.SaveChangesAsync();

            await this.appDbContext
                .Users
                .Where(x => group.UserIds.Contains(x.Id))
                .ForEachAsync(x =>
                {
                    x.GroupId = toSave.Id;
                });
            try
            {
                await this.appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return toSave.Id;
        }

        public async Task DeleteGroup(int id)
        {
            // TODO: will this delete all the groups
            this.appDbContext.Groups.Remove(new Data.Models.Group
            {
                Id = id
            });

            await this.appDbContext.SaveChangesAsync();
        }
    }
}
