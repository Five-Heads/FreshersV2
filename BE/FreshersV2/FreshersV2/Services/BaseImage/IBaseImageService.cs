using FreshersV2.Data.Models.BlurredImageGame;

namespace FreshersV2.Services.BaseImage
{
    public interface IBaseImageService
    {
        Task Add(string base64image, string objectName);
    }
}
