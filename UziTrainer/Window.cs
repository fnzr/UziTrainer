using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace UziTrainer
{
    public class Window
    {
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
        private static Bitmap bitmap =  new Bitmap(1,1);
        public static readonly Rectangle FullArea = new Rectangle(0, 0, 1284, 754);


        public static void Init()
        {
            var swc = Properties.Settings.Default.ScreenWindowClass;
            var swt = Properties.Settings.Default.ScreenWindowTitle;
            var mwc = Properties.Settings.Default.MessageWindowClass;
            var mwt = Properties.Settings.Default.MessageWindowTitle;
            var hwnd = Message.FindWindow(swc, swt);
            if (hwnd <= 0)
            {
                throw new ArgumentException(String.Format("No HWND for window [{0}]", swt));
            }
            Window.WindowHWND = hwnd;
            var mhwnd = Message.FindWindowEx(hwnd, 0, mwc, mwt);
            Window.MessageHWND = mhwnd;
            Window.WindowHWNDPtr = new IntPtr(hwnd);
            Window.MessageHWNDPtr = new IntPtr(mhwnd);
            Message.ShowWindow(WindowHWNDPtr, Message.SW_RESTORE);
        }

        public static Bitmap CaptureBitmap()
        {
            return _CaptureBitmap();
        }

        public static Bitmap CaptureBitmap(Rectangle searchArea)
        {   
            var bmp = _CaptureBitmap();
//#if DEBUG
            //var path = Path.Combine(Constants.DebugDir, DateTime.UtcNow.ToString("yyyyMMdd_HHmmssffffff") + ".png");
            //bmp.Save(path, ImageFormat.Png);
            //Tracer.TraceEvent(TraceEventType.Verbose, Constants.TraceDebug, "Captured [{0}]", path);
//#endif
try
            {
                return bmp.Clone(searchArea, bmp.PixelFormat);

            }catch(OutOfMemoryException e)
            {
                Trace.TraceError("Provided area is out of bounds! Capturing entire image. Fix this. Provided area is: " + searchArea.ToString());
                return bmp;
            }
        }

        private static Bitmap _CaptureBitmap()
        {
            RECT rc;
            Message.GetWindowRect(WindowHWNDPtr, out rc);
            DebugForm.Reference = rc;

            bitmap.Dispose();
            bitmap = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bitmap);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            Message.PrintWindow(WindowHWNDPtr, hdcBitmap, 0x1);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bitmap;
        }
    }
}
