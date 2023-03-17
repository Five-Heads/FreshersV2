using FreshersV2.Data;
using FreshersV2.Models.TreasureHunt.Continue;
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

        public async Task<ContinueTreasureHuntResponseModel> GetContinueTreasureHuntModel(int treasureHuntId)
        {
            var result = new ContinueTreasureHuntResponseModel();



            return result;
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
