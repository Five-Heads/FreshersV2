namespace FreshersV2.Data.Models.VoteImageGame
{
    public class RoundDrawingUser
    {
        public int ContestId { get; set; }

        public VoteImageContest Contest { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public string UserId { get; set; }

        public UserContest User { get; set; }
    }
}