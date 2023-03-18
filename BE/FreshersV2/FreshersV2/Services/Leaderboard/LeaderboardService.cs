using FreshersV2.Data;
using Microsoft.EntityFrameworkCore;

namespace FreshersV2.Services.Leaderboard
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly AppDbContext context;

        public LeaderboardService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Data.Models.Leaderboard>> All()
        {
            return await context.Leaderboard.AsNoTracking().ToListAsync();
        }

        public async Task AddPoints(string userId, int score)
        {
            var leader = await context.Leaderboard.FirstOrDefaultAsync(x => x.UserId == userId);
            if (leader == null)
            {
                var leaderboard = new Data.Models.Leaderboard { Score = score, UserId = userId };
                await this.context.Leaderboard.AddAsync(leaderboard);
            }
            else
            leader.Score += score;

            await context.SaveChangesAsync();
        }
    }
}
