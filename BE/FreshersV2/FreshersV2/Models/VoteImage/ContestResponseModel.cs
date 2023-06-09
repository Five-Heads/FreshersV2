﻿namespace FreshersV2.Models.VoteImage
{
    public class ContestResponseModel
    {
        public int ContestId { get; set; }

        public string Name { get; set; }

        public int MaxParticipants { get; set; }

        public int UsersCount { get; set; }

        public int VoteTime { get; set; }

        public int DrawTime { get; set; }

        public List<string> Words { get; set; }
    }
}
