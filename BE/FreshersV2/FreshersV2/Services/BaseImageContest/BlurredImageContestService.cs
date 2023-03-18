using FreshersV2.Data;
using FreshersV2.Data.Models.BlurredImageGame;
using FreshersV2.Enums;
using FreshersV2.Helpers;
using FreshersV2.Models.BlurredImage;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BlurredImage;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FreshersV2.Services.BaseImageContest
{
    public class BlurredImageContestService : IBlurredImageContestService
    {
        private readonly IBaseImageService baseImageService;
        private readonly AppDbContext appDbContext;

        public BlurredImageContestService(AppDbContext appDbContext, IBaseImageService baseImageService)
        {
            this.appDbContext = appDbContext;
            this.baseImageService = baseImageService;
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
    }
}
