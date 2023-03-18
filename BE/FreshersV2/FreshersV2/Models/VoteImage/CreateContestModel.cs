namespace FreshersV2.Models.VoteImage
{
    public class CreateContestModel
    {
        public string Name { get; set; }
        public int MaxParticipants { get; set; }
        public int VoteTime { get; set; }
        public int DrawTime { get; set; }
        public List<string> Words { get; set; }
    }
}
