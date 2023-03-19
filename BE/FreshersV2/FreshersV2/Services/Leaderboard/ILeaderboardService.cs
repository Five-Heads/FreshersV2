using FreshersV2.Models.Leaderboard;

namespace FreshersV2.Services.Leaderboard
{
    public interface ILeaderboardService
    {
        Task<List<LeaderboardEntityResponseModel>> All();

        Task AddPoints(string userId, int score);
    }
}
