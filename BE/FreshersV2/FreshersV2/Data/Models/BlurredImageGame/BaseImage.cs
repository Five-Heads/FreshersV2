using System.ComponentModel.DataAnnotations;

namespace FreshersV2.Data.Models.BlurredImageGame
{
    public class BaseImage
    {
        [Key]
        public int Id { get; set; }

        public string Object { get; set; } // the object in the image ( the correct answer for the contest )

        public string Base64Image { get; set; }

        public List<BlurredImage> BlurredImages { get; set; }

        public List<BlurredImageContest> Contests { get; set; }
    }
}