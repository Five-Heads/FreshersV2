﻿using FreshersV2.Data;
using FreshersV2.Models.Group.Create;

namespace FreshersV2.Services.Group
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext appDbContext;

        public GroupService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task CreateGroup(CreateGroupRequestModel group)
        {
            var toSave = new Data.Models.Group
            {
                Name = group.Name,
            };

            // TODO: check if it works
            foreach (var userId in group.UserIds)
            {
                toSave.Users.Add(new Data.Models.UserGroup { UserId = userId });
            }
            
            await this.appDbContext.Groups.AddAsync(toSave);
            await this.appDbContext.SaveChangesAsync();
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