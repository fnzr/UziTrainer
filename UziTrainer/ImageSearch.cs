using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace UziTrainer
{
    public class ImageSearch
    {
        private readonly LockedBitmap haystack;
        private readonly LockedBitmap needle;
        private readonly int pixelFormatSize;

        private ImageSearch(Bitmap _needle, Bitmap _haystack)
        {
            haystack = new LockedBitmap(_haystack);
            needle = new LockedBitmap(_needle);
            pixelFormatSize = Image.GetPixelFormatSize(_needle.PixelFormat) / 8;
        }

        public static void Find(String imagePath, int[] area)
        {
            //return Find(new Bitmap(imagePath), Window.CaptureBitmap(area), 20, out _);
        }

        public static bool Exists(String imagePath, int[] area)
        {
            return Exists(imagePath, area, out _);
        }

        public static bool Exists(String imagePath, int[] area, out Point coordinates)
        {
            return Search(imagePath, Window.CaptureBitmap(area), out coordinates);
        }

        private static bool Search(String needlePath, Bitmap haystack, out Point coordinates)
        {
            return Search(new Bitmap(needlePath), haystack, 20, out coordinates);
        }

        private static bool Search(Bitmap bmpFind, Bitmap bmpSource, int tolerance, out Point coordinates)
        {
            #region Arguments check

            if (bmpSource == null || bmpFind == null)
                throw new ArgumentNullException();
            if (bmpSource.PixelFormat != bmpFind.PixelFormat)
                throw new ArgumentException("Pixel formats aren't equal");
            if (bmpSource.Width < bmpFind.Width || bmpSource.Height < bmpFind.Height)
                throw new ArgumentException("Size of serchingBitmap bigger then sourceBitmap");

            #endregion            
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
                    if (!IsSameColor(haystackIndex, needleIndex, tolerance)) {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsSameColor(int haystackIndex, int needleIndex, int tolerance)
        {
            for(int i = 0; i < pixelFormatSize; i++)
            {
                if (Math.Abs(haystack.bytes[haystackIndex + i] - needle.bytes[needleIndex + i]) > tolerance) {
                    return false;
                }
            }
            return true;
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
