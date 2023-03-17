using FreshersV2.Data;
using Microsoft.EntityFrameworkCore;

namespace FreshersV2.Services.TreasureHunt
{
    public class TreasureHuntService : ITreasureHuntService
    {
        private readonly AppDbContext appDbContext;

        public TreasureHuntService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        async Task<List<Data.Models.TreasureHunt>> ITreasureHuntService.GetUserTreasureHunts(string userId)
        {
            return await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.UserId == userId)
                .Select(x => x.TreasureHunt)
                .ToListAsync();
        }
    }
}
