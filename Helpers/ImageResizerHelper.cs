using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NwsTerritoryMigration.Helpers
{
    public class ImageResizeHelper
    {
        public static (byte[] FileContents, int Height, int Width) Resize(byte[] fileContents,
          int maxWidth, int maxHeight,
          SKFilterQuality quality = SKFilterQuality.High)
        {
            using MemoryStream ms = new MemoryStream(fileContents);
            using SKBitmap sourceBitmap = SKBitmap.Decode(ms);

            int height = Math.Min(maxHeight, sourceBitmap.Height);
            int width = Math.Min(maxWidth, sourceBitmap.Width);

            using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
            using SKData data = scaledImage.Encode(SKEncodedImageFormat.Jpeg, 50);

            return (data.ToArray(), height, width);
        }
    }
}
