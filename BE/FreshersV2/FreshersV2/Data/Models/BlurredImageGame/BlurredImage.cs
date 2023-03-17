using System.ComponentModel.DataAnnotations;

namespace FreshersV2.Data.Models.BlurredImageGame
{
    public class BlurredImage
    {
        [Key]
        public int Id { get; set; }

        public int BlurrLevel { get; set; }

        public string Base64Image { get; set; }

        public int BaseImageId { get; set; }

        public BaseImage BaseImage { get; set; }
    }
}