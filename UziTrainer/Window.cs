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
    class Window
    {
        readonly IntPtr windowHWND;
        readonly IntPtr messageHWND;

        public Window(
            String screenWindowClass,
            String screenWindowTitle,
            String messageWindowClass,
            String messageWindowTitle)
        {
            var hwnd = Win32.FindWindow(screenWindowClass, screenWindowTitle);
            if (hwnd <= 0) {
                throw new ArgumentException(String.Format("No HWND for window [{0}]", screenWindowTitle));
            }
            windowHWND = new IntPtr(hwnd);
            var mhwnd = Win32.FindWindowEx(hwnd, 0, messageWindowClass, messageWindowTitle);
            messageHWND = new IntPtr(mhwnd);
        }

        public Bitmap CaptureBitmap()
        {
            var x = _CaptureBitmap();
            x.Save(@"C:\Users\master\Pictures\Screenshot_4.png");
            return x;
        }

        public Bitmap CaptureBitmap(Rectangle area)
        {
            var bmp = _CaptureBitmap();
            return bmp.Clone(area, bmp.PixelFormat);
        }

        private Bitmap _CaptureBitmap()
        {
            RECT rc;
            Win32.GetWindowRect(windowHWND, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            Win32.PrintWindow(windowHWND, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bmp;
        }

        public bool ImageExists(String imagePath, Rectangle area, out Point coordinates)
        {
            var source = CaptureBitmap(area);
            var search = new Bitmap(imagePath);
            return FindPoint(source, search, 0, out coordinates);
        }

        public bool ImageExists(String imagePath, out Point coordinates)
        {
            var source = CaptureBitmap();
            var search = new Bitmap(imagePath);
            return FindPoint(source, search, 0, out coordinates);
        }

        public bool ImageExists(String imagePath)
        {
            return ImageExists(imagePath, out _);
        }

        private bool FindPoint(Bitmap sourceBitmap, Bitmap needleBitmap, int tolerance, out Point coordinates)
        {
            #region Arguments check

            if (sourceBitmap == null || needleBitmap == null)
                throw new ArgumentNullException();
            if (sourceBitmap.PixelFormat != needleBitmap.PixelFormat)
                throw new ArgumentException("Pixel formats aren't equal");
            if (sourceBitmap.Width < needleBitmap.Width || sourceBitmap.Height < needleBitmap.Height)
                throw new ArgumentException("Size of serchingBitmap bigger then sourceBitmap");

            #endregion

            var pixelFormatSize = System.Drawing.Image.GetPixelFormatSize(PixelFormat.Format32bppArgb) / 8;

            LockedBitmap haystack = new LockedBitmap(sourceBitmap);
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

                    var isEqual = true;
                    for (var c = 0; c < pixelFormatSize; c++)
                    {// through the bytes in pixel
                        if (haystack.bytes[sourceX + sourceY + c] == needle.bytes[c])
                            continue;
                        isEqual = false;
                        break;
                    }

                    if (!isEqual) continue;
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

        private class LockedBitmap {
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
