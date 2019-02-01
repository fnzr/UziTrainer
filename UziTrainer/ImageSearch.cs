using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace UziTrainer
{
    public class ImageSearch
    {
        private const int DEFAULT_TOLERANCE = 20;
        private readonly LockedBitmap haystack;
        private readonly LockedBitmap needle;
        private readonly int pixelFormatSize;

        private ImageSearch(Bitmap _needle, Bitmap _haystack)
        {
            haystack = new LockedBitmap(_haystack);
            needle = new LockedBitmap(_needle);
            pixelFormatSize = Image.GetPixelFormatSize(_needle.PixelFormat) / 8;
        }

        public static bool FindPoint(Query query, out Point coordinates)
        {
            DebugForm.DebugSearch(query.Area);
            var found = Search(query.Image, Window.CaptureBitmap(query.Area), query.Tolerance, out coordinates);
            if (found)
            {
                coordinates.X += query.Area.X;
                coordinates.Y += query.Area.Y;
            }
            return found;
        }

        private static bool Search(Image<Rgba, byte> needle, Image<Rgba, byte> haystack, float threshold, out Point coordinates)
        {
            using (Image<Gray, float> result = haystack.MatchTemplate(needle, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] maxValues;
                Point[] maxLocations;
                result.MinMax(out _, out maxValues, out _, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] >= threshold)
                {
                    coordinates = maxLocations[0];
                    return true;
                }
            }
            coordinates = Point.Empty;
            return false;
        }

        private static bool _Search(Bitmap bmpFind, Bitmap bmpSource, int tolerance, out Point coordinates)
        {
            var search = new ImageSearch(bmpFind, bmpSource);
            var haystack = search.haystack;
            var needle = search.needle;

            for (var mainY = 0; mainY < bmpSource.Height - bmpFind.Height + 1; mainY++)
            {
                var sourceY = mainY * haystack.data.Stride;
                for (var mainX = 0; mainX < bmpSource.Width - bmpFind.Width + 1; mainX++)
                {
                    var sourceX = mainX * search.pixelFormatSize;
                    if (search.RegionSearch(sourceX, sourceY, tolerance))
                    {
                        coordinates = new Point(mainX, mainY);
                        return true;
                    }
                }
            }
            coordinates = Point.Empty;
            return false;
        }

        private bool RegionSearch(int startX, int startY, int tolerance)
        {
            for (var y = 0; y < needle.bitmap.Height; y++)
            {
                var needleIndexY = y * needle.data.Stride;
                var haystackIndexY = startY + (y * haystack.data.Stride);
                for (var x = 0; x < needle.bitmap.Width; x++)
                {
                    var needleIndexX = x * pixelFormatSize;
                    var haystackIndexX = needleIndexX + startX;

                    var haystackIndex = haystackIndexX + haystackIndexY;
                    var needleIndex = needleIndexX + needleIndexY;
                    if (!IsSameColor(haystackIndex, needleIndex, tolerance))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsSameColor(int haystackIndex, int needleIndex, int tolerance)
        {
            for (int i = 0; i < pixelFormatSize; i++)
            {
                if (Math.Abs(haystack.bytes[haystackIndex + i] - needle.bytes[needleIndex + i]) > tolerance)
                {
                    return false;
                }
            }
            return true;
        }

        public void Dispose()
        {

        }

        private class LockedBitmap
        {
            public readonly Bitmap bitmap;
            public readonly BitmapData data;
            public readonly byte[] bytes;

            public LockedBitmap(Bitmap bitmap)
            {
                this.bitmap = bitmap;
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, bitmap.PixelFormat);
                var sourceBitmapBytesLength = data.Stride * bitmap.Height;
                bytes = new byte[sourceBitmapBytesLength];
                Marshal.Copy(data.Scan0, bytes, 0, sourceBitmapBytesLength);
                bitmap.UnlockBits(data);
            }
        }
    }
}
