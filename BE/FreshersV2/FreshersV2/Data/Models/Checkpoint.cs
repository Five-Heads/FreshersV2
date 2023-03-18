namespace FreshersV2.Data.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Question { get; set; }

        public bool IsFinal { get; set; }

        public string AssignedPersonName { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string QRCode { get; set; }

        public int OrderNumber { get; set; }

        public int TreasureHuntId { get; set; }

        public TreasureHunt TreasureHunt { get; set; }
    }
}
