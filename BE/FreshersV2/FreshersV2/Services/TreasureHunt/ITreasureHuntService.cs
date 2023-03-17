namespace FreshersV2.Services.TreasureHunt
{
    public interface ITreasureHuntService
    {
        Task<List<Data.Models.TreasureHunt>> GetUserTreasureHunts(string userId);
    }
}
