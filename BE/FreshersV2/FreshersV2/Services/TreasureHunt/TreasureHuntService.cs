using FreshersV2.Data;
using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.Start;
using Microsoft.EntityFrameworkCore;
using QRCoder;

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
            var result = await appDbContext.TreasureHunts.AddAsync(new Data.Models.TreasureHunt
            {
                Name = model.Name,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Checkpoints = model.Checkpoints.Select(x => new Data.Models.Checkpoint
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

            await
                this.appDbContext
                .Checkpoints
                .Where(x => x.TreasureHuntId == result.Entity.Id)
                .ForEachAsync(x =>
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{x.Id}/{result.Entity.Id}", QRCodeGenerator.ECCLevel.Q);
                    PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                    x.QRCode = System.Text.Encoding.UTF8.GetString(qrCode.GetGraphic(20));
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
                .UserTreasureHunts
                .Where(x => x.User.GroupId == result.GroupId && x.NextId != result.Next.Id)
                .Select(x => x.UserId)
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

        public async Task AssignGroupToTreasureHunt(int treasureHuntId, int groupId)
        {
            var treasureHunt = await
                this.appDbContext
                .TreasureHunts
                .FindAsync(treasureHuntId);

            if (treasureHunt == null)
            {
                return;
            }

            await this.appDbContext
                .GroupTreasureHunts
                .AddAsync(new Data.Models.GroupTreasureHunt
                {
                    TreasureHuntId = treasureHuntId,
                    GroupId = groupId,
                    Started = false,
                    Done = null,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow,
                    NextId = treasureHunt.Checkpoints.Min(x => x.OrderNumber)
                });

            await this.appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateNextCheckpointForUser(int checkpointId, int treasureHuntId, string userId)
        {
            var nextId = await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.UserId == userId && x.TreasureHuntId == treasureHuntId)
                .Select(x => x.NextId)
                .FirstOrDefaultAsync();

            return nextId == checkpointId;
        }

        public async Task UpdateNextCheckpointForUser(int treasureHuntId, string userId)
        {
            var current = await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.TreasureHuntId == treasureHuntId && x.UserId == userId)
                .FirstOrDefaultAsync();

            if (current == null)
            {
                return;
            }

            var next = await this.appDbContext
                .Checkpoints
                .Where(x => x.TreasureHuntId == treasureHuntId && x.OrderNumber == current.Next.OrderNumber + 1)
                .FirstOrDefaultAsync();

            if (next == null)
            {
                return;
            }

            current.NextId = next.Id;
            this.appDbContext.Update(current);
            await this.appDbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfAllHaveReachedCheckpoint(int groupId, int treasureHuntId)
        {
            var groupTreasureHunt = await this.appDbContext
                .GroupTreasureHunts
                .Where(x => x.GroupId == groupId)
                .FirstOrDefaultAsync();

            var haveReached = await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.User.GroupId == groupId && x.NextId == groupTreasureHunt.NextId)
                .CountAsync();

            if (haveReached > 0)
            {
                return false;
            }

            var newNext = await this.appDbContext
                .Checkpoints
                .FirstOrDefaultAsync(x => x.TreasureHuntId == treasureHuntId && x.OrderNumber == groupTreasureHunt.Next.OrderNumber + 1);

            // TODO: distinct
            if (newNext == null)
            {
                return false;
            }

            groupTreasureHunt.NextId = newNext.Id;
            this.appDbContext.Update(groupTreasureHunt);
            await this.appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
