namespace FreshersV2.Services.BlurredImage
{
    public interface IBlurredImageService
    {
        Task CreateBlurredImages(string base64image, int baseImageId);
    }
}
