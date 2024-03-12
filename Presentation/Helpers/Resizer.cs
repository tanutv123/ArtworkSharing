using System.Drawing.Imaging;
using System.Drawing;

namespace Presentation.Helpers
{
    public class Resizer
    {
        public static async Task<byte[]> ResizeImageFromUrlAsync(string imageUrl)
        {
            const int targetWidth = 1920;
            const int targetHeight = 1080;

            using (var httpClient = new HttpClient())
            {
                // Download the image data
                var imageData = await httpClient.GetByteArrayAsync(imageUrl);

                using (var ms = new MemoryStream(imageData))
                using (var sourceImage = Image.FromStream(ms))
                {
                    var resizedImage = new Bitmap(targetWidth, targetHeight);
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        // Set the resize quality modes to high
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        // Calculate the ratio to resize the image
                        var ratioX = (double)targetWidth / sourceImage.Width;
                        var ratioY = (double)targetHeight / sourceImage.Height;
                        var ratio = Math.Min(ratioX, ratioY);

                        var newWidth = (int)(sourceImage.Width * ratio);
                        var newHeight = (int)(sourceImage.Height * ratio);

                        // Draw the image into the target bitmap
                        graphics.DrawImage(sourceImage, (targetWidth - newWidth) / 2, (targetHeight - newHeight) / 2, newWidth, newHeight);
                    }

                    using (var resultStream = new MemoryStream())
                    {
                        // Save the resized image to the stream
                        resizedImage.Save(resultStream, ImageFormat.Jpeg);
                        return resultStream.ToArray();
                    }
                }
            }
        }
    }
}
