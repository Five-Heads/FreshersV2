using FreshersV2.Enums;
using System.ComponentModel.DataAnnotations;

namespace FreshersV2.Data.Models.BlurredImageGame
{
    public class BlurredImageContest
    {
        [Key]
        public int Id { get; set; }

        public int MaxParticipants { get; set; }

        public int SecondsPerRound { get; set; }

        public int BaseImageId { get; set; }

        public BaseImage BaseImage { get; set; }

        public int Status { get; set; }     
    }
}