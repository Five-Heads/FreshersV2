using FreshersV2.Data;
using FreshersV2.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

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
            try
            {
                if (base64image != null)
                {
                    if (!string.IsNullOrEmpty(base64image))
                    {
                        Image viewIcon = Base64StringToImage(base64image);

                        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64image)))
                        {
                            using (Bitmap bm2 = new Bitmap(ms))
                            {
                                for (int i = 9; i < 15; i++)
                                {
                                    var bitmap = Blur(bm2, i);
                                    var base64 = ToBase64String(bitmap, viewIcon.RawFormat);

                                    string compressedBase64 = ImageHelper.GetCompressedBase64Image(base64);

                                    var blurredImage = new FreshersV2.Data.Models.BlurredImageGame.BlurredImage
                                    {
                                        Base64Image = compressedBase64,
                                        BaseImageId = baseImageId,
                                        BlurrLevel = i,
                                    };

                                    await appDbContext.BlurredImages.AddAsync(blurredImage);
                                };

                                await appDbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private Image Base64StringToImage(string base64ImageString)
        {
            byte[] b;
            b = Convert.FromBase64String(base64ImageString);

            MemoryStream ms = new System.IO.MemoryStream(b);
            Image img = System.Drawing.Image.FromStream(ms);

            return img;
        }

        private string ToBase64String(Bitmap bmp, ImageFormat format)
        {
            string base64String = string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, format);

            memoryStream.Position = 0;
            byte[] byteBuffer = memoryStream.ToArray();

            memoryStream.Close();

            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;

            return base64String;
        }

        // This method exits because visual studio shits itself with erros. (without this transition method)
        private static Bitmap Blur(Bitmap image, Int32 blurSize)
        {
            return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
        }

        private unsafe static Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            BitmapData blurredData = blurred.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, blurred.PixelFormat);

            int bitsPerPixel = Image.GetPixelFormatSize(blurred.PixelFormat);

            // Get pointer to first line
            byte* scan0 = (byte*)blurredData.Scan0.ToPointer();

            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            // Get pointer to RGB
                            byte* data = scan0 + y * blurredData.Stride + x * bitsPerPixel / 8;

                            avgB += data[0]; // Blue
                            avgG += data[1]; // Green
                            avgR += data[2]; // Red

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                    {
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                        {
                            byte* data = scan0 + y * blurredData.Stride + x * bitsPerPixel / 8;

                            data[0] = (byte)avgB;
                            data[1] = (byte)avgG;
                            data[2] = (byte)avgR;
                        }
                    }
                }
            }

            blurred.UnlockBits(blurredData);

            return blurred;
        }
    }
}
