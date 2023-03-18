using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.Start;

namespace FreshersV2.Services.TreasureHunt
{
    public interface ITreasureHuntService
    {
        Task<List<Data.Models.TreasureHunt>> GetUserTreasureHunts(string userId);

        Task<StartTreasureHuntResponseModel> StartTreasureHunt(int treasureHuntId, string userId);

        Task CreateTreasureHunt(CreateTreasureHuntRequestModel model);
    }
}
