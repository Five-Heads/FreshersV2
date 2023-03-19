using FreshersV2.Data;
using FreshersV2.Data.Models;
using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.NextCheckpoint;
using FreshersV2.Models.TreasureHunt.Start;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;

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
                    QRCode = ""
                }).ToList()
            });

            await appDbContext.SaveChangesAsync();

            await this.appDbContext
                .Checkpoints
                .Where(x => x.TreasureHuntId == result.Entity.Id)
                .ForEachAsync(x =>
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{x.Id}/{result.Entity.Id}", QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    using (var ms = new MemoryStream())
                    {
                        using (var bitmap = new Bitmap(qrCodeImage))
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                            x.QRCode = SigBase64;
                        }
                    }
                });


            await appDbContext.SaveChangesAsync();
        }

        public async Task<StartTreasureHuntResponseModel> StartTreasureHunt(int treasureHuntId, string userId)
        {

            var result = await this.appDbContext
                .UserTreasureHunts
                .Include(x => x.Next)
                .Include(x => x.TreasureHunt)
                .ThenInclude(x => x.Checkpoints)
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
                .Where(x => x.TreasureHuntId == treasureHuntId && x.User.GroupId == result.GroupId && x.NextId != result.Next.Id)
                .Select(x => x.UserId)
                .ToListAsync();

            return result;
        }

        async Task<List<Data.Models.TreasureHunt>> ITreasureHuntService.GetUserTreasureHunts(string userId)
        {
            return await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.UserId == userId && !x.Done)
                .Select(x => x.TreasureHunt)
                .ToListAsync();
        }

        public async Task AssignGroupToTreasureHunt(int treasureHuntId, int groupId)
        {
            var treasureHunt = await
                this.appDbContext
                .TreasureHunts
                .Include(x => x.Checkpoints)
                .FirstOrDefaultAsync(x => x.Id == treasureHuntId);

            if (treasureHunt == null)
            {
                return;
            }

            var firstCheckpoint = treasureHunt.Checkpoints
                .OrderBy(x => x.OrderNumber)
                .FirstOrDefault();

            await this.appDbContext
                .GroupTreasureHunts
                .AddAsync(new Data.Models.GroupTreasureHunt
                {
                    TreasureHuntId = treasureHuntId,
                    GroupId = groupId,
                    Started = false,
                    Done = false,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow,
                    NextId = firstCheckpoint.Id
                });


            var users = await this.appDbContext
                .Users
                .Where(x => x.GroupId == groupId)
                .ToListAsync();

            if (users.Count > 0)
            {
                List<UserTreasureHunt> userTreasureHunts = new List<UserTreasureHunt>();

                foreach (var user in users)
                {
                    userTreasureHunts.Add(new UserTreasureHunt
                    {
                        TreasureHuntId = treasureHuntId,
                        UserId = user.Id,
                        NextId = firstCheckpoint.Id,
                        Done = false
                    });
                }

                await this.appDbContext
                    .UserTreasureHunts
                    .AddRangeAsync(userTreasureHunts);
            }

            await this.appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateNextCheckpointForUser(int checkpointId, int treasureHuntId, string userId)
        {
            var nextId = await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.UserId == userId && x.TreasureHuntId == treasureHuntId)
                .Select(x => x.NextId)
                .FirstOrDefaultAsync();

            if (nextId != checkpointId)
            {
                return false;
            }

            await this.appDbContext
                .Leaderboard
                .Where(x => x.UserId == userId)
                .ForEachAsync(x => { x.Score += 20; });

            return true;
        }

        public async Task UpdateNextCheckpointForUser(int treasureHuntId, string userId)
        {
            var current = await this.appDbContext
                .UserTreasureHunts
                .Include(x => x.Next)
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
                current.Done = true;
            }
            else
            {
                current.NextId = next.Id;
            }

            this.appDbContext.Update(current);
            await this.appDbContext.SaveChangesAsync();
        }

        public async Task<NextCheckpointResponseModel> CheckIfAllHaveReachedCheckpoint(int groupId, int treasureHuntId)
        {
            var groupTreasureHunt = await this.appDbContext
                .GroupTreasureHunts
                .Where(x => x.GroupId == groupId && x.TreasureHuntId == treasureHuntId)
                .FirstOrDefaultAsync();

            var haveReached = await this.appDbContext
                .UserTreasureHunts
                .Where(x => x.User.GroupId == groupId && x.NextId == groupTreasureHunt.NextId)
                .CountAsync();

            if (haveReached > 0)
            {
                return null;
            }

            var newNext = await this.appDbContext
                .Checkpoints
                .FirstOrDefaultAsync(x => x.TreasureHuntId == treasureHuntId && x.OrderNumber == groupTreasureHunt.Next.OrderNumber + 1);

            // TODO: distinct
            if (newNext == null)
            {
                groupTreasureHunt.Done = true;
                this.appDbContext.Update(groupTreasureHunt);
                await this.appDbContext.SaveChangesAsync();
                return null;
            }

            groupTreasureHunt.NextId = newNext.Id;
            this.appDbContext.Update(groupTreasureHunt);
            await this.appDbContext.SaveChangesAsync();

            return new NextCheckpointResponseModel
            {
                Id = newNext.Id,
                IsFinal = newNext.IsFinal,
                AssignedPerson = newNext.AssignedPersonName,
                Name = newNext.Name,
                OrderNumber = newNext.OrderNumber,
                Question = newNext.Question
            };
        }

        public async Task<List<Data.Models.TreasureHunt>> GetAllTreasureHunts()
        {
            return await this.appDbContext
                .TreasureHunts
                .ToListAsync();
        }
    }
}
