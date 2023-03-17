using FreshersV2.Models.TreasureHunt.Continue;

namespace FreshersV2.Services.TreasureHunt
{
    public interface ITreasureHuntService
    {
        Task<List<Data.Models.TreasureHunt>> GetUserTreasureHunts(string userId);

        Task<ContinueTreasureHuntResponseModel> GetContinueTreasureHuntModel(int treasureHuntId);
    }
}
