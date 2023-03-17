namespace FreshersV2.Data.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public List<UserGroup> Users = new List<UserGroup>();


        public List<GroupTreasureHunt> TreasureHunts = new List<GroupTreasureHunt>();
    }
}
