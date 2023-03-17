using FreshersV2.Models.BlurredImage;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BlurredImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace FreshersV2.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class BaseImageController : BaseApiController
    {
        private readonly IBaseImageService baseImageService;
        private readonly IBlurredImageService blurredImageService;

        public BaseImageController(IBlurredImageService blurredImageService, IBaseImageService baseImageService)
        {
            this.blurredImageService = blurredImageService;
            this.baseImageService = baseImageService;
        }

        [HttpPost("add")]
        public async void AddBaseImage(AddBaseImageRequestModel model) // just for test
        {
            // TODO: move to service
            try
            {
                if (model != null && !string.IsNullOrEmpty(model.Base64Image))
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(model.Base64Image)))
                    {
                        using (Bitmap bm2 = new Bitmap(ms))
                        {
                            Bitmap blurred = new Bitmap(bm2, new Size(64, 64));
                            Bitmap test;
                            blurred.SetResolution(64, 64);

                            int currI = 0;
                            int currJ = 0;

                            int newI = 0;
                            int newJ = 0;

                            int increment = 1;

                            string color1;
                            string color2;
                            string color3;
                            string color4;

                            while (currI < blurred.Height)
                            {
                                while (currJ < blurred.Width)
                                {
                                    var c1 = blurred.GetPixel(currI, currJ).ToString();
                                    var c2 = blurred.GetPixel(currI + increment, currJ).ToString();
                                    var c3 = blurred.GetPixel(currI, currJ + increment).ToString();
                                    var c4 = blurred.GetPixel(currI + increment, currJ + increment).ToString();

                                    int c1Alpha = 
                                    //blurred.SetPixel(0, 0, Color.Blue);
                                    //var c2 = blurred.GetPixel(0, 0);

                                    currJ += increment * 2;
                                }

                                currI += increment * 2;
                            }
                        }
                    }

                    //   await this.baseImageService.Add(model.Base64Image);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
