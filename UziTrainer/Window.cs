using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
    }
}
