using FreshersV2.Infrastructure.Extensions;
using FreshersV2.Services.Group;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace FreshersV2.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TreasureHuntHub : Hub
    {
        private readonly IGroupService groupService;

        // userId: connectionId
        public static readonly ConcurrentDictionary<string, string> ConnectionsMap
            = new ConcurrentDictionary<string, string>();


        // groupId: active users from this group
        public static readonly ConcurrentDictionary<int, List<string>> ActiveGroupsMap
            = new ConcurrentDictionary<int, List<string>>();

        public TreasureHuntHub(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public override async Task OnConnectedAsync()
        {
            // add user to connection map
            await this.UserConnected();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // remove user from connection map
            await this.UserDisconnected();
            await base.OnDisconnectedAsync(exception);
        }

        #region User Activity

        private async Task UserConnected()
        {
            var userId = this.Context.User.GetUserId();
            var connectionId = this.Context.ConnectionId;

            ConnectionsMap.TryAdd(userId, connectionId);

            var group = await groupService.GetUserGroup(userId);
            await this.Groups.AddToGroupAsync(connectionId, group.Id.ToString());
            if (ActiveGroupsMap.TryGetValue(group.Id, out var users))
            {
                users.Add(userId);
            }
            else
            {
                ActiveGroupsMap.TryAdd(group.Id, new List<string>
                {
                    userId
                });
            }
        }

        private async Task UserDisconnected()
        {
            var userId = this.Context.User.GetUserId(); 
            
            if (ConnectionsMap.TryRemove(userId, out var connectionId))
            {
                var group = await groupService.GetUserGroup(userId);
                await this.Groups.RemoveFromGroupAsync(connectionId, group.Id.ToString());
                if (ActiveGroupsMap.TryGetValue(group.Id, out var users))
                {
                    users.Remove(userId);

                    if (users.Count == 0)
                    {
                        ActiveGroupsMap.TryRemove(group.Id, out var _);
                    }
                }
            }
        }

        #endregion
    }
}
