using System.ComponentModel.DataAnnotations;

namespace FreshersV2.Data.Models.VoteImageGame
{
    public class VoteImageContest
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxParticipants { get; set; }

        public int VoteTime { get; set; }

        public int DrawTime { get; set; }

        //Comma separated
        public string Words { get; set; }

        public List<User> Participants { get; set; } = new List<User>();
        public List<Round> Rounds { get; set; } = new List<Round>();
        public List<VoteImage> Images { get; set; } = new List<VoteImage>();
        public List<UserContest> UserContests { get; set; } = new List<UserContest>();
    }
}