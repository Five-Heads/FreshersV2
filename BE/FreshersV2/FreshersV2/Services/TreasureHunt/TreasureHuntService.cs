using FreshersV2.Data;
using FreshersV2.Models.TreasureHunt.Start;
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

        public async Task<StartTreasureHuntResponseModel> StartTreasureHunt(int treasureHuntId, string userId)
        {
            var result = await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.UserId == userId && x.TreasureHuntId == treasureHuntId)
                .Select(x => new StartTreasureHuntResponseModel()
                {
                    Id = x.TreasureHuntId,
                    TotalCheckpoints = x.TreasureHunt.Checkpoints.Count,
                    Next = new Models.TreasureHunt.NextCheckpoint.NextCheckpointResponseModel
                    {
                        Id = x.Next.Id,
                        Name = x.Next.Name,
                        IsFinal = x.Next.IsFinal,
                        AssignedPerson = x.Next.AssignedPersonName,
                        OrderNumber = x.Next.OrderNumber,
                        Question = x.Next.Question
                    }
                })
                .FirstOrDefaultAsync();


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
