using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    class Window
    {
        private readonly static TraceSource Tracer = new TraceSource("fnzr.UziTrainer");
        public static int MessageHWND
        {
            get;
            private set;
        }
        public static int WindowHWND
        {
            get;
            private set;
        }
        public static IntPtr MessageHWNDPtr
        {
            get;
            private set;
        }
        public static IntPtr WindowHWNDPtr
        {
            get;
            private set;
        }

        public static void init()
        {
            var swc = Properties.Settings.Default.ScreenWindowClass;
            var swt = Properties.Settings.Default.ScreenWindowTitle;
            var mwc = Properties.Settings.Default.MessageWindowClass;
            var mwt = Properties.Settings.Default.MessageWindowTitle;
            var hwnd = Win32.FindWindow(swc, swt);
            if (hwnd <= 0)
            {
                throw new ArgumentException(String.Format("No HWND for window [{0}]", swt));
            }
            Window.WindowHWND = hwnd;
            var mhwnd = Win32.FindWindowEx(hwnd, 0, mwc, mwt);
            Window.MessageHWND = mhwnd;
            Window.WindowHWNDPtr = new IntPtr(hwnd);
            Window.MessageHWNDPtr = new IntPtr(mhwnd);
            Win32.ShowWindow(WindowHWNDPtr, Win32.SW_RESTORE);
        }

        public static Bitmap CaptureBitmap()
        {
            return _CaptureBitmap();
        }

        public static Bitmap CaptureBitmap(int[] area)
        {            
            if (area.Length != 4) {
                throw new ArgumentException("Capture area requires exactly 4 values: StartX, StartY, EndX, EndY");
            }
            var bmp = _CaptureBitmap();
#if DEBUG
            var path = Path.Combine(Constants.DebugDir, DateTime.UtcNow.ToString("yyyyMMdd_HHmmssffffff") + ".png");
            bmp.Save(path, ImageFormat.Png);
            Tracer.TraceEvent(TraceEventType.Verbose, Constants.TraceDebug, "Captured [{0}]", path);
#endif
            return bmp.Clone(new Rectangle(area[0], area[1], area[2], area[3]), bmp.PixelFormat);
        }

        private static Bitmap _CaptureBitmap()
        {
            RECT rc;
            Win32.GetWindowRect(WindowHWNDPtr, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            Win32.PrintWindow(WindowHWNDPtr, hdcBitmap, 0x1);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bmp;
        }
    }
}
