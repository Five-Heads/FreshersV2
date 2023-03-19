using FreshersV2.Data;
using FreshersV2.Data.Models.VoteImageGame;
using FreshersV2.Hubs;
using FreshersV2.Models.VoteImage;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FreshersV2.Services.ImageVote
{
    public class ImageVoteService : IImageVoteService
    {
        private readonly AppDbContext context;

        public ImageVoteService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CastVote(int contestId, int roundId, string userId, int imageId)
        {
            var roundVotes = await context.RoundVotes.Include(x=>x.VoteImageRound.Images).FirstOrDefaultAsync(x =>
                x.RoundId == roundId && x.VoteImageRound.Images.Any(a => a.Id == imageId));

            var index = roundVotes.VoteImageRound.Images.FindIndex(x => x.Id == imageId);
            if (index == 1)
            {
                roundVotes.Image1Votes++;
            }
            else if (index == 2)
            {
                roundVotes.Image2Votes++;
            }

            await context.SaveChangesAsync();
        }

        public async Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime, List<string> words)
        {
            await context.Contests.AddAsync(new VoteImageContest
            {
                DrawTime = drawTime,
                VoteTime = voteTime,
                MaxParticipants = maxParticipants,
                Name = name,
                Words = string.Join(',', words),
            });

            await context.SaveChangesAsync();
        }

        public async Task CreateRound(int contestId, int roundNumber, string word, List<string> drawingUsersHubIds)
        {
            await context.Rounds.AddAsync(new Round
            {
                ContestId = contestId,
                RoundNumber = roundNumber,
                Word = word,
                DrawingUsers = context.RoundDrawingUsers.Where(x => drawingUsersHubIds.Contains(x.User.UserHubId)).ToList(),
            });
            await context.SaveChangesAsync();
        }

        public async Task<RoundVote> GetRoundVote(int voteRoundId)
        {
            return await context.RoundVotes.FirstOrDefaultAsync(x => x.Id == voteRoundId);
        }

        public async Task<int> CreateVoteRound(int contestId, int roundNumber, VoteImage image1, VoteImage image2)
        {
            var result = new RoundVote
            {
                RoundId = (await context.Rounds.FirstOrDefaultAsync(x =>
                    x.ContestId == contestId && x.RoundNumber == roundNumber))?.Id ?? 0,
                Image1Votes = 0,
                Image2Votes = 0,
                VoteImageRound = new VoteImageRound
                {
                    Images = new List<VoteImage> { image1, image2 },
                }
            };

            await context.RoundVotes.AddAsync(result);
            await context.SaveChangesAsync();
            return result.Id;
        }

        public async Task<VoteImageContest> GetContest(int contestId)
        {
            return await context.FindAsync<VoteImageContest>(contestId);
        }

        public Task<List<UserContest>> GetInitialDrawingUsers(int contestId, int currentRoundId)
        {
            return context.UserContests.Where(x => x.ContestId == contestId).ToListAsync();
        }

        public List<ContestResponseModel> AllContests()
        {
            return context.Contests.Select(x => new ContestResponseModel
            {
                MaxParticipants = x.MaxParticipants,
                DrawTime = x.DrawTime,
                ContestId = x.Id,
                Name = x.Name,
                VoteTime = x.VoteTime,
                Words = x.Words.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                UsersCount =0 //VoteImageHub.ContestConnectionsMap.GetOrAdd(x.Id.ToString(),new List<string>()).Count,
            }).ToList();
        }

        public async Task<List<VoteImage>> GetRoundImages(int contestId, int roundId)
        {
            return await context.Images.Where(x => x.ContestId == contestId && x.RoundVoteId == roundId)
                .Include(x => x.User).ToListAsync();
        }

        public async Task SaveImage(int contestId, int roundId, string userId, string imageBase64)
        {
            await context.Images.AddAsync(new VoteImage()
            {
                ContestId = contestId,
                Base64Image = imageBase64,
                UserId = userId,
                RoundVoteId = roundId
            });

            await context.SaveChangesAsync();
        }
    }
}
