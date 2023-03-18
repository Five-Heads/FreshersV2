namespace FreshersV2.Models.Group
{
    public class GroupInfoResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserResponseModel> Users { get; set; } = new List<UserResponseModel>();
    }
}
