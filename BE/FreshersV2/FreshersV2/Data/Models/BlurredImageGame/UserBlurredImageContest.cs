namespace FreshersV2.Data.Models.BlurredImageGame
{
    public class UserBlurredImageContest
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int BlurredImageContestId { get; set; }

        public BlurredImageContest BlurredImageContest { get; set; }

        public int Ranking { get; set; }

        public int Points { get; set; }
    }
}