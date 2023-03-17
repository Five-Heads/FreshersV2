namespace FreshersV2.Data.Models
{
    public class GroupTreasureHunt
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public int TreasureHuntId { get; set; }

        public TreasureHunt TreasureHunt { get; set; }

        public string Done { get; set; }

        public int NextId { get; set; }

        public Checkpoint Next { get; set; }

        public bool Started { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
