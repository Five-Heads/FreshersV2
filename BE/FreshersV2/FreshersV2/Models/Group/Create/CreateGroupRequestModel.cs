namespace FreshersV2.Models.Group.Create
{
    public class CreateGroupRequestModel
    {
        public string Name { get; set; }

        public List<string> UserIds { get; set; }

        public int TreasureHuntId { get; set; }
    }
}
