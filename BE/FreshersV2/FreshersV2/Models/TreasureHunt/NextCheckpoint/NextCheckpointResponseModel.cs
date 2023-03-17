namespace FreshersV2.Models.TreasureHunt.NextCheckpoint
{
    public class NextCheckpointResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Question { get; set; }

        public string AssignedPerson { get; set; }

        public bool IsFinal { get; set; }

        public int OrderNumber { get; set; }
    }
}
