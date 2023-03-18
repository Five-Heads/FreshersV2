using FreshersV2.Data;
using FreshersV2.Services.BlurredImage;
using System.Drawing.Imaging;
using System.Drawing;
using System.Buffers.Text;
using FreshersV2.Helpers;

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

        public async Task Add(string base64image, string objectName)
        {
            try
            {
                string compressedBase64 = ImageHelper.GetCompressedBase64Image(base64image);

                var image = new FreshersV2.Data.Models.BlurredImageGame.BaseImage
                {
                    Base64Image = compressedBase64,
                    Object = objectName,
                };

                appDbContext.Add(image);

                await appDbContext.SaveChangesAsync();

                await this.blurredImageService.CreateBlurredImages(base64image, image.Id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
