using FreshersV2.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FreshersV2.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestHub : Hub
    {
        // userId: connectionId
        public static readonly ConcurrentDictionary<string, string> ConnectionsMap
            = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            // add user to connection map
            this.UserConnected();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // remove user from connection map
            this.UserDisconnected();
            await base.OnDisconnectedAsync(exception);
        }

        #region User Activity

        private void UserConnected()
        {
            var userId = this.Context.User.GetUserId();
            var connectionId = this.Context.ConnectionId;

            ConnectionsMap.TryAdd(userId, connectionId);
        }

        private void UserDisconnected()
        {
            var userId = this.Context.User.GetUserId();
            ConnectionsMap.TryRemove(userId, out var connectionId);
        }

        #endregion
    }
}
