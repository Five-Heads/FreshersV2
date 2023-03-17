using System.ComponentModel.DataAnnotations;

namespace FreshersV2.Data.Models.VoteImageGame
{
    public class Round
    {
        [Key]
        public int Id { get; set; }

        public string Word { get; set; }

        public int RoundNumber { get; set; }

        public int ContestId { get; set; }

        public VoteImageContest Contest { get; set; }

        public List<VoteImage> Images { get; set; } = new List<VoteImage>();

        public List<RoundVote> Votes { get; set; } = new List<RoundVote>();

        public List<RoundDrawingUser> DrawingUsers { get; set; } = new List<RoundDrawingUser>();

        public List<VoteImageRound> ImageRounds { get; set; } = new List<VoteImageRound>();

    }
}