namespace FreshersV2.Data.Models.VoteImageGame
{
    public class VoteImage
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public UserContest User { get; set; }

        public int VoteImageRoundId { get; set; }

        public VoteImageRound VoteImageRound { get; set; }

        public int ContestId { get; set; }

        public VoteImageContest Contest { get; set; }

        public int RoundVoteId { get; set; }

        public RoundVote Vote { get; set; }

        public string Base64Image { get; set; }
    }
}