using FreshersV2.Data.Models.BlurredImageGame;
using FreshersV2.Models.BlurredImage;

namespace FreshersV2.Services.BaseImageContest
{
    public interface IBlurredImageContestService
    {
        Task CreateContest(CreateBlurredImageContestRequestModel model);

        Task<BlurredImageContest?> GetUpcomingContest();
    }
}
