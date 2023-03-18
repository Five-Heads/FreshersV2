namespace FreshersV2.Models.TreasureHunt.Create
{
    public class CreateTreasureHuntRequestModel
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<CreateCheckpointRequestModel> Checkpoints { get; set; } = new List<CreateCheckpointRequestModel>();
    }
}
