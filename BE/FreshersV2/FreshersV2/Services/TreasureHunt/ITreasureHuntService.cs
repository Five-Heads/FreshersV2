using FreshersV2.Models.TreasureHunt.Create;
using FreshersV2.Models.TreasureHunt.NextCheckpoint;
using FreshersV2.Models.TreasureHunt.Start;

namespace FreshersV2.Services.TreasureHunt
{
    public interface ITreasureHuntService
    {
        Task<List<Data.Models.TreasureHunt>> GetUserTreasureHunts(string userId);

        Task<StartTreasureHuntResponseModel> StartTreasureHunt(int treasureHuntId, string userId);

        Task CreateTreasureHunt(CreateTreasureHuntRequestModel model);

        Task AssignGroupToTreasureHunt(int treasureHuntId, int groupId);

        Task<bool> ValidateNextCheckpointForUser(int checkpointId, int treasureHuntId, string userId);

        Task UpdateNextCheckpointForUser(int treasureHuntId, string userId);

        Task<NextCheckpointResponseModel> CheckIfAllHaveReachedCheckpoint(int groupId,  int checkpointId);
        Task<List<Data.Models.TreasureHunt>> GetAllTreasureHunts();
    }
}
