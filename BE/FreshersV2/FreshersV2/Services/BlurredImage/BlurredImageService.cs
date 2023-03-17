using FreshersV2.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FreshersV2.Services.BlurredImage
{
    public class BlurredImageService : IBlurredImageService
    {
        private readonly AppDbContext appDbContext;

        public BlurredImageService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task CreateBlurredImages(string base64image, int baseImageId)
        {
            // var baseImage = await this.appDbContext.BaseImages.FirstOrDefaultAsync(x => x.Id == baseImageId);

            if (base64image != null)
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64image)))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save("SavingPath" + "ImageName.jpg");
                    }
                }
            }


            throw new NotImplementedException();
        }
    }
}
