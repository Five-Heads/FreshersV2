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

        private async Task UpdateContestsData()
        {
            await Clients.All.SendAsync("ContestsUpdateData",
                ContestConnectionsMap.Select(x => new { ContestId=x.Key, UsersCount=x.Value.Count }).ToList());
        }
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

        public async Task JoinContest(int contestId)
        {
            var contest = await imageVoteService.GetContest(contestId);
            if (ContestConnectionsMap.GetOrAdd(contestId.ToString(), new List<string>()).Count>=contest.MaxParticipants)
            {
                return;
            }

            await Groups.AddToGroupAsync(this.Context.ConnectionId, contestId.ToString());
            ContestConnectionsMap.AddOrUpdate(contestId.ToString(), new List<string> { this.Context.User.GetUserId() }, (key, value) =>
            {
                value.Add(this.Context.User.GetUserId());
                return value;
            });

            await UpdateContestsData();
        }

        public async Task LeaveContest(int contestId)
        {
            await Groups.RemoveFromGroupAsync(this.Context.ConnectionId, contestId.ToString());
            ContestConnectionsMap.AddOrUpdate(contestId.ToString(), new List<string> { this.Context.User.GetUserId() }, (key, value) =>
            {
                value.Remove(this.Context.User.GetUserId());
                return value;
            });

            await Clients.All.SendAsync("ContestsUpdateData",
                ContestConnectionsMap.Select(x => new { x.Key, x.Value.Count }).ToList());

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