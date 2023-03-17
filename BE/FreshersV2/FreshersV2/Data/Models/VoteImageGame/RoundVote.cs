using System.ComponentModel.DataAnnotations;

namespace FreshersV2.Data.Models.VoteImageGame
{
    public class RoundVote
    {
        [Key]
        public int Id { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public int Image1Id { get; set; }

        public VoteImage Image1 { get; set; }

        public int Image2Id { get; set; }

        public VoteImage Image2 { get; set; }

        public int Image1Votes { get; set; }

        public int Image2Votes { get; set; }
    }
}