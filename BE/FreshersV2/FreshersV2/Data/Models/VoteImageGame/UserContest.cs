namespace FreshersV2.Data.Models.VoteImageGame
{
    public class UserContest
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string UserHubId { get; set; }

        public int ContestId { get; set; }

        public VoteImageContest Contest { get; set; }

        public List<VoteImage> Images { get; set; } = new List<VoteImage>();
        public List<RoundDrawingUser> RoundsDrawing { get; set; } = new List<RoundDrawingUser>();
    }
}