namespace FreshersV2.Data.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public List<User> Users = new List<User>();


        public List<GroupTreasureHunt> TreasureHunts = new List<GroupTreasureHunt>();
    }
}
