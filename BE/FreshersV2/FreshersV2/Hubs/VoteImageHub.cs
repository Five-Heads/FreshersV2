using System.Collections.Concurrent;
using FreshersV2.Infrastructure.Extensions;
using FreshersV2.Services.ImageVote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FreshersV2.Hubs
{
    [Authorize]
    public class VoteImageHub : Hub
    {
        private const string JoinContestRequest = "JoinContest";
        private const string StartRoundRequest = "StartRound";

        private readonly IImageVoteService imageVoteService;

        public VoteImageHub(IImageVoteService imageVoteService)
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

        [HubMethodName(StartRoundRequest)]
        public async Task SendStartRound(string contestId, string roundId)
        {
            // TODO: create ROUND
            // TODO: send specific WillDraw info
            await Clients.Group(contestId).SendAsync(StartRoundRequest, roundId);
        }

        public async Task SendEndRound(string contestId, string roundId)
        => await Clients.Group(contestId).SendAsync("EndRound", roundId);

        public async Task StartVote(string contestId, string roundId)
        => await Clients.Group(contestId).SendAsync("StartVOte", roundId);


        public async Task VoteForImage()
        {
            await Clients.Group("contestId").SendAsync("VoteForImage", "roundId", "IM1", "IM2");
        }

        public async Task CastVote(string contestId, int roundId, string imageId)
        {
            var userId = this.Context.User.GetUserId();
            await this.imageVoteService.CastVote(contestId, roundId, imageId, userId);
        }

        public async Task EndVote(string contestId, string roundId)
        {
            // TODO: Show vote results
            await Clients.Group(contestId).SendAsync("EndVote", roundId);
        }

        public async Task EndContest(string contestId)
        {
            await Clients.Group(contestId).SendAsync("EndContest", contestId);
        }

        public async Task SendImage(string contestId, int roundId, string imageBase64)
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