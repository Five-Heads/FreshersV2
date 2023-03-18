using FreshersV2.Data;
using FreshersV2.Data.Models.BlurredImageGame;
using FreshersV2.Enums;
using FreshersV2.Helpers;
using FreshersV2.Models.BlurredImage;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BlurredImage;
using FreshersV2.Services.Leaderboard;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FreshersV2.Services.BaseImageContest
{
    public class BlurredImageContestService : IBlurredImageContestService
    {
        private readonly IBaseImageService baseImageService;
        private readonly ILeaderboardService leaderboardService;
        private readonly AppDbContext appDbContext;

        public BlurredImageContestService(AppDbContext appDbContext, IBaseImageService baseImageService, ILeaderboardService leaderboardService)
        {
            this.appDbContext = appDbContext;
            this.baseImageService = baseImageService;
            this.leaderboardService = leaderboardService;
        }

        public async Task CreateContest(CreateBlurredImageContestRequestModel model)
        {
            var baseImage = await this.baseImageService.GetRandomBaseImage();

            if (baseImage != null)
            {
                var contest = new BlurredImageContest
                {
                    MaxParticipants = model.MaxParticipants,
                    SecondsPerRound = model.SecondsPerRound,
                    BaseImageId = baseImage.Id,
                    BaseImage = baseImage,
                    Status = (int)ContestStatus.Upcoming,
                };

                var notCompletedContests = await this.appDbContext.BlurredImageContests.Where(x => x.Status != (int)ContestStatus.Completed).ToListAsync();
                if (notCompletedContests != null)
                {
                    foreach (var item in notCompletedContests)
                    {
                        if (item.Status != (int)ContestStatus.Completed)
                        {
                            item.Status = (int)ContestStatus.Completed;
                            this.appDbContext.BlurredImageContests.Update(item);
                        }
                    }
                }


                this.appDbContext.BlurredImageContests.Add(contest);
                await this.appDbContext.SaveChangesAsync();
            }
        }

        public async Task<BlurredImageContest?> GetUpcomingContest()
        {
            var contest = await this.appDbContext.BlurredImageContests
                .AsNoTracking()
                .Include(x => x.UserBlurredImageContests)
                .Include(x => x.BaseImage)
                .ThenInclude(x => x.BlurredImages)
                .FirstOrDefaultAsync(x => x.Status == (int)ContestStatus.Upcoming);

            if (contest != null && contest.BaseImage != null && contest.BaseImage.BlurredImages != null)
            {
                foreach (var blurredImage in contest.BaseImage.BlurredImages)
                {
                    blurredImage.Base64Image = ImageHelper.GetDecompressedBase64Image(blurredImage.Base64Image);
                }
            }

            return contest;
        }

        public async Task AddUserToUpcomingContest(string userId)
        {
            var contest = await this.appDbContext.BlurredImageContests
                .AsNoTracking()
                .Include(x=>x.UserBlurredImageContests)
                .FirstOrDefaultAsync(x => x.Status == (int)ContestStatus.Upcoming);

            if (contest != null && contest.UserBlurredImageContests != null && contest.MaxParticipants < contest.UserBlurredImageContests.Count)
            {
                var userContest = new UserBlurredImageContest
                {
                    UserId = userId,
                    BlurredImageContestId = contest.Id,
                };

                await this.appDbContext.UserBlurredImageContests.AddAsync(userContest);
                await this.appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetUpcomingContestUsers()
        {
            return await this.appDbContext.UserBlurredImageContests
                .AsNoTracking()
                .Where(x => x.BlurredImageContest.Id == (int)ContestStatus.Upcoming)
                .Select(x => x.UserId)
                .ToListAsync();
        }

        public async Task AddUsersPointsToLeaderboard(List<BlurredImageContestResultsRequestModel> results)
        {
            results = results.OrderBy(x => x.Completed).ToList();
            if (results != null && results.Any())
            {
                for (int i = 1; i <= results.Count; i++)
                {
                    if (results[i].Completed != null)
                    {
                        var leaderboard = await this.appDbContext.Leaderboard
                            .FirstOrDefaultAsync(x => x.UserId == results[i].UserId);

                        bool isNew = leaderboard == null;
                        if (leaderboard == null)
                        {
                            leaderboard = new Data.Models.Leaderboard
                            {
                                UserId = results[i].UserId,
                                Score = 0
                            };
                        }

                        if (i == 1)
                            leaderboard.Score = 50;
                        else if (i == 2)
                            leaderboard.Score = 35;
                        else if (i == 3)
                            leaderboard.Score = 20;
                        else
                            break;

                        if (isNew)
                            await this.appDbContext.Leaderboard.AddAsync(leaderboard);
                        else
                            this.appDbContext.Leaderboard.Update(leaderboard);

                        await this.appDbContext.SaveChangesAsync();
                    };
                }
            }
        }
    }
}