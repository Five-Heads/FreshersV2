namespace FreshersV2.Data.Models.VoteImageGame
{
    public class VoteImageRound
    {
        public int Id { get; set; }

        public RoundVote RoundVote { get; set; }

        public List<VoteImage> Images { get; set; }

    }
}
