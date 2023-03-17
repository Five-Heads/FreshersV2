namespace FreshersV2.Data.Models
{
    public class TreasureHunt
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
    }
}
