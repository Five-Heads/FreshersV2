namespace FreshersV2.Services.Leaderboard
{
    public interface ILeaderboardService
    {
        Task<List<Data.Models.Leaderboard>> All();

        Task AddPoints(string userId, int score);
    }
}
