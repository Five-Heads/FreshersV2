namespace FreshersV2.Data.Models
{
    public class Leaderboard
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int Score { get; set; }
    }
}
