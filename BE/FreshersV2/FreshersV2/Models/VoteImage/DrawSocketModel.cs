namespace FreshersV2.Models.VoteImage
{
    public class DrawSocketModel
    {
        public int ContestId { get; set; }
        public int CurrendRoundId { get; set; }
        public int DrawTime { get; set; }
        public string Word { get; set; }
        public bool IsDrawing { get; set; }
    }
}
