namespace FreshersV2.Models.TreasureHunt.Create
{
    public class CreateCheckpointRequestModel
    {
        public string Name { get; set; }

        public string Question { get; set; }

        public int OrderNumber { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string AssignedPersonName { get; set; }
    }
}
