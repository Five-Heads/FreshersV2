using FreshersV2.Models.Group;
using FreshersV2.Models.TreasureHunt.NextCheckpoint;

namespace FreshersV2.Models.TreasureHunt.Start
{
    public class StartTreasureHuntResponseModel
    {
        public int Id { get; set; }

        public NextCheckpointResponseModel Next { get; set; }

        public List<string> NextReachedBy { get; set; } = new List<string>(); // ids

        public int TotalCheckpoints { get; set; }

        public int? GroupId { get; set; }
    }
}
