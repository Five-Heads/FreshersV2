using FreshersV2.Data;
using FreshersV2.Models.Leaderboard;
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

        public async Task<List<LeaderboardEntityResponseModel>> All()
        {
            return await context.Leaderboard
                .Include(x => x.User)
                .Select(x => new LeaderboardEntityResponseModel
                {
                    Id = x.UserId,
                    Name = x.User.UserName,
                    FacultyNumber = x.User.FacultyNumber,
                    Points = x.Score
                })
                .AsNoTracking()
                .ToListAsync();
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
