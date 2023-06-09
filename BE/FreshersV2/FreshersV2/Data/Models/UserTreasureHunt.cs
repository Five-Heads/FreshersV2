﻿namespace FreshersV2.Data.Models
{
    public class UserTreasureHunt
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int TreasureHuntId { get; set; }

        public TreasureHunt TreasureHunt { get; set; }

        public bool Done { get; set; } = false;

        public int NextId { get; set; }

        public Checkpoint Next { get; set; }
    }
}
