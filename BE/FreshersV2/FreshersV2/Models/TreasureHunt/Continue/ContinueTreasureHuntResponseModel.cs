using FreshersV2.Models.Group;
using FreshersV2.Models.TreasureHunt.NextCheckpoint;

namespace FreshersV2.Models.TreasureHunt.Continue
{
    public class ContinueTreasureHuntResponseModel
    {
        public int Id { get; set; }

        public NextCheckpointResponseModel Next { get; set; }

        public List<int> DoneIds { get; set; } // comma-separated string

        public List<string> NextReachedBy { get; set; } = new List<string>(); // ids

        public int TotalCheckpoints { get; set; }

        public int GroupId { get; set; }
    }
}
