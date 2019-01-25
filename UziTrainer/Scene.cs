using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    static class Scene
    {
        public static void Wait(Query query)
        {

        }

        public static bool Exists(Query query, int timeout)
        {
            return false;
        }

        public static void Click(Query query)
        {

        }

        public static void Click(int x, int y)
        {
            Mouse.Click(x, y);
        }

        private static bool FindPoint(Bitmap sourceBitmap, Bitmap needleBitmap, int tolerance, out Point coordinates)
        {
            #region Arguments check

            if (sourceBitmap == null || needleBitmap == null)
                throw new ArgumentNullException();
            if (sourceBitmap.PixelFormat != needleBitmap.PixelFormat)
                throw new ArgumentException("Pixel formats aren't equal");
            if (sourceBitmap.Width < needleBitmap.Width || sourceBitmap.Height < needleBitmap.Height)
                throw new ArgumentException("Size of serchingBitmap bigger then sourceBitmap");

            #endregion            
            var pixelFormatSize = Image.GetPixelFormatSize(sourceBitmap.PixelFormat) / 8;

            LockedBitmap haystack = new LockedBitmap(sourceBitmap);
            //AssemblyLoadEventHandler red green blue
            LockedBitmap needle = new LockedBitmap(needleBitmap);

            // Serching entries
            // minimazing serching zone
            // sourceBitmap.Height - serchingBitmap.Height + 1
            for (var mainY = 0; mainY < sourceBitmap.Height - needleBitmap.Height + 1; mainY++)
            {
                var sourceY = mainY * haystack.data.Stride;
                for (var mainX = 0; mainX < sourceBitmap.Width - needleBitmap.Width + 1; mainX++)
                {// mainY & mainX - pixel coordinates of sourceBitmap
                 // sourceY + sourceX = pointer in array sourceBitmap bytes
                    var sourceX = mainX * pixelFormatSize;
                    var index = sourceX + sourceY;
                    if (!IsSameColor(haystack.bytes, needle.bytes, index, 0, tolerance))
                    {
                        continue;
                    }
                    var isStop = false;

                    // find fist equalation and now we go deeper) 
                    for (var secY = 0; secY < needleBitmap.Height; secY++)
                    {
                        var serchY = secY * needle.data.Stride;
                        var sourceSecY = (mainY + secY) * haystack.data.Stride;
                        for (var secX = 0; secX < needleBitmap.Width; secX++)
                        {// secX & secY - coordinates of serchingBitmap
                         // serchX + serchY = pointer in array serchingBitmap bytes

                            var serchX = secX * pixelFormatSize;

                            var sourceSecX = (mainX + secX) * pixelFormatSize;

                            for (var c = 0; c < pixelFormatSize; c++)
                            {// through the bytes in pixel
                                if (haystack.bytes[sourceSecX + sourceSecY + c] == needle.bytes[serchX + serchY + c]) continue;
                                // not equal - abort iteration
                                isStop = true;
                                break;
                            }

                            if (isStop) break;
                        }

                        if (isStop) break;
                    }

                    if (!isStop)
                    {// serching bitmap is founded!!
                        coordinates = new Point(mainX, mainY);
                        return true;
                    }
                }
            }
            coordinates = Point.Empty;
            return false;
        }

        private static bool IsSameColor(byte[] haystack, byte[] needle, int haystackIndex, int needleIndex, int tolerance)
        {
            return !(Math.Abs(haystack[haystackIndex] - needle[needleIndex]) > tolerance ||
                        Math.Abs(haystack[haystackIndex + 1] - needle[needleIndex + 1]) > tolerance ||
                        Math.Abs(haystack[haystackIndex + 2] - needle[needleIndex + 2]) > tolerance);
        }

        // private isSameColor()

        private class LockedBitmap
        {
            public readonly BitmapData data;
            public readonly byte[] bytes;

            public LockedBitmap(Bitmap bitmap)
            {
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
