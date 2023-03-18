using FreshersV2.Models.BlurredImage;
using FreshersV2.Services.BaseImage;
using FreshersV2.Services.BlurredImage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace FreshersV2.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task AddBaseImage(AddBaseImageRequestModel model)
        {
            await this.baseImageService.Add(model.Base64Image, model.Object);
        }
    }
}
