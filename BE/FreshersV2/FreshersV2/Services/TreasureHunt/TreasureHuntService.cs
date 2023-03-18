using FreshersV2.Data;
using FreshersV2.Models.TreasureHunt.Create;
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

        public async Task CreateTreasureHunt(CreateTreasureHuntRequestModel model)
        {
            var finalOrderNumber = model.Checkpoints.Max(x => x.OrderNumber);
            await appDbContext.TreasureHunts.AddAsync(new Data.Models.TreasureHunt
            {
                Name = model.Name,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Checkpoints = model.Checkpoints.Select(x=> new Data.Models.Checkpoint
                {
                    Name = x.Name,
                    Question = x.Question,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    OrderNumber = x.OrderNumber,
                    AssignedPersonName = x.AssignedPersonName,
                    IsFinal = x.OrderNumber == finalOrderNumber,
                }).ToList()
            });

            await appDbContext.SaveChangesAsync();
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
                    },
                    GroupId = x.User.GroupId,
                })
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return null;
            }

            result.NextReachedBy = await this.appDbContext
                .Users
                .Where(x => x.GroupId == result.GroupId)
                .Select(x => x.Id)
                .ToListAsync();

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
