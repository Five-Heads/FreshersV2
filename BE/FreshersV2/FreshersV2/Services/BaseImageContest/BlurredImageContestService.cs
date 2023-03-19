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
            try
            {
                var baseImage = await this.baseImageService.GetRandomBaseImage();

                if (baseImage != null)
                {
                    var contest = new BlurredImageContest
                    {
                        MaxParticipants = model.MaxParticipants,
                        SecondsPerRound = model.SecondsPerRound,
                        BaseImageId = baseImage.Id,
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
            catch (Exception ex)
            {
            }
        }

        public async Task ChangeStatus(int status)
        {
            var contest = await this.appDbContext.BlurredImageContests
                .FirstOrDefaultAsync(x => x.Status == (int)ContestStatus.Upcoming);
            contest.Status = status;
            this.appDbContext.BlurredImageContests.Update(contest);
            await this.appDbContext.SaveChangesAsync();
        }

        public async Task<BlurredImageContest?> GetUpcomingContest()
        {
            try
            {
                var contest = await this.appDbContext.BlurredImageContests
                                //.AsNoTracking()
                                //.Include(x => x.UserBlurredImageContests)
                                .Include(x => x.BaseImage)
                                // .ThenInclude(x => x.BlurredImages)
                                .FirstOrDefaultAsync(x => x.Status == (int)ContestStatus.Upcoming);

                var blurredImages = await this.appDbContext.BlurredImages
                        .Where(x => x.BaseImageId == contest.BaseImageId)
                        .ToListAsync();
                var userContests = await this.appDbContext.UserBlurredImageContests
                        .Where(x => x.BlurredImageContestId == contest.Id)
                        .ToListAsync();
                contest.UserBlurredImageContests = userContests;
                contest.BaseImage.BlurredImages = blurredImages;
                contest.BaseImage.Contests = null;

                if (contest != null && contest.BaseImage != null && contest.BaseImage.BlurredImages != null)
                {
                    foreach (var blurredImage in contest.BaseImage.BlurredImages)
                    {
                        blurredImage.Base64Image = ImageHelper.GetDecompressedBase64Image(blurredImage.Base64Image);
                        blurredImage.BaseImage = null;
                    }
                }

                if (contest != null && contest.UserBlurredImageContests != null)
                {
                    foreach (var con in contest.UserBlurredImageContests)
                    {
                        con.BlurredImageContest = null;
                    }
                }

                return contest;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task AddUserToUpcomingContest(string userId)
        {
            try
            {
                var contest = await this.appDbContext.BlurredImageContests
                                .AsNoTracking()
                                .Include(x => x.UserBlurredImageContests)
                                .FirstOrDefaultAsync(x => x.Status == (int)ContestStatus.Upcoming);

                if (contest != null)
                {
                    if ((contest.UserBlurredImageContests != null && contest.MaxParticipants > contest.UserBlurredImageContests.Count)
                        || contest.UserBlurredImageContests == null)
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
            }
            catch (Exception ex)
            {
            }

        }

        public async Task<List<string>> GetUpcomingContestUsers()
        {
            return await this.appDbContext.UserBlurredImageContests
                .AsNoTracking()
                .Include(x => x.BlurredImageContest)
                .Where(x => x.BlurredImageContest.Status == (int)ContestStatus.Upcoming)
                .Select(x => x.UserId)
                .ToListAsync();
        }

        public async Task AddUsersPointsToLeaderboard(int round, string userId)
        {
            try
            {
                var leaderboard = await this.appDbContext.Leaderboard
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                bool isNew = leaderboard == null;
                if (leaderboard == null)
                {
                    leaderboard = new Data.Models.Leaderboard
                    {
                        UserId = userId,
                        Score = (6-round)*10
                    };
                }
                else
                   leaderboard.Score += (6-round)*10;

                if (isNew)
                    await this.appDbContext.Leaderboard.AddAsync(leaderboard);
                else
                    this.appDbContext.Leaderboard.Update(leaderboard);

                await this.appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }

        }
    }
}