using FreshersV2.Data;
using FreshersV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshersV2.Services.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<UserResponseModel>> GetUsersWithoutGroup()
        {
            return await this.appDbContext
                .Users
                .Where(x => !x.GroupId.HasValue)
                .Select(x => new UserResponseModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FacultyNumber = x.FacultyNumber
                })
                .ToListAsync();
        }
    }
}
