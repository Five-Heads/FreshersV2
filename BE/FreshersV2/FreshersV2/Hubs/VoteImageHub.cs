using System.Collections.Concurrent;
using FreshersV2.Infrastructure.Extensions;
using FreshersV2.Jobs;
using FreshersV2.Services.ImageVote;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Hubs
{
    [Authorize]
    public class VoteImageHub : Hub
    {
        private readonly IImageVoteService imageVoteService;

        public VoteImageHub(IImageVoteService imageVoteService, VoteRoundJob voteRoundJob)
        {
            this.imageVoteService = imageVoteService;
        }

        // userId: connectionId
        public static readonly ConcurrentDictionary<string, string> ConnectionsMap = new();

        // contestId: userIds  
        public static readonly ConcurrentDictionary<string, List<string>> ContestConnectionsMap = new();

        public override async Task OnConnectedAsync()
        {
            this.UserConnected();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            this.UserDisconnected();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinContest(string contestId)
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, contestId);
            ContestConnectionsMap.AddOrUpdate(contestId, new List<string> { this.Context.User.GetUserId() }, (key, value) =>
            {
                value.Add(this.Context.User.GetUserId());
                return value;
            });
        }

        public async Task LeaveContest()
        {
            var contestId = ContestConnectionsMap.FirstOrDefault(x => x.Value.Contains(this.Context.User.GetUserId())).Key;
            await Groups.RemoveFromGroupAsync(this.Context.ConnectionId, contestId);
            ContestConnectionsMap.AddOrUpdate(contestId, new List<string> { this.Context.User.GetUserId() }, (key, value) =>
            {
                value.Remove(this.Context.User.GetUserId());
                return value;
            });
        }

        public async Task CastVote(int contestId, int roundId, int imageId)
        {
            var userId = this.Context.User.GetUserId();
            await this.imageVoteService.CastVote(contestId, roundId, userId, imageId);
        }

        public async Task SendImage(int contestId, int roundId, string imageBase64)
        {
            var userId = this.Context.User.GetUserId();
            await this.imageVoteService.SaveImage(contestId, roundId, userId, imageBase64);
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