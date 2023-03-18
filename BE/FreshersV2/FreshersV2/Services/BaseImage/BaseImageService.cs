using FreshersV2.Data;
using FreshersV2.Services.BlurredImage;
using System.Drawing.Imaging;
using System.Drawing;
using System.Buffers.Text;
using FreshersV2.Helpers;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Data.Models.BlurredImageGame.BaseImage?> GetRandomBaseImage()
        {
            var baseImagesCount = await this.appDbContext.BaseImages.CountAsync();

            Random rnd = new Random();
            int number = rnd.Next(1, baseImagesCount);

            var baseImage = await this.appDbContext.BaseImages.Include(x=>x.BlurredImages).FirstOrDefaultAsync(x => x.Id == number);

            return baseImage;
        }
    }
}
