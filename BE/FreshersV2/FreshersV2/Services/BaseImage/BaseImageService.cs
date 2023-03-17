using FreshersV2.Data;
using FreshersV2.Services.BlurredImage;

namespace FreshersV2.Services.BaseImage
{
    public class BaseImageService : IBaseImageService
    {
        private readonly IBlurredImageService blurredImageService;
        private readonly AppDbContext appDbContext;

        public BaseImageService(IBlurredImageService blurredImageService, AppDbContext appDbContext)
        {
            this.blurredImageService = blurredImageService;
            this.appDbContext = appDbContext;
        }

        public async Task Add(string base64image)
        {
            var image = new FreshersV2.Data.Models.BlurredImageGame.BaseImage
            {
                Base64Image = base64image,
                Object = "test",
            };

            appDbContext.Add(image);

            await appDbContext.SaveChangesAsync();

            await this.blurredImageService.CreateBlurredImages(base64image, image.Id);
        }
    }
}
