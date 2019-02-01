using Emgu.CV;
using Emgu.CV.Structure;
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
        private static Image<Rgba, byte> Image = new Image<Rgba, byte>(1,1);
        public static readonly Rectangle FullArea = new Rectangle(0, 0, 1284, 754);
        public static Rectangle WindowRectangle
        {
            get {
                RECT rc;
                Message.GetWindowRect(WindowHWNDPtr, out rc);
                return rc;
            }
            private set { }
        }


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

        public static Image<Rgba, byte> CaptureBitmap()
        {
            return _CaptureBitmap();
        }

        public static Image<Rgba, byte> CaptureBitmap(Rectangle searchArea)
        {   
            var img = _CaptureBitmap();
//#if DEBUG
            //var path = Path.Combine(Constants.DebugDir, DateTime.UtcNow.ToString("yyyyMMdd_HHmmssffffff") + ".png");
            //bmp.Save(path, ImageFormat.Png);
            //Tracer.TraceEvent(TraceEventType.Verbose, Constants.TraceDebug, "Captured [{0}]", path);
//#endif
try
            {
                return img.Copy(searchArea);

            }catch(OutOfMemoryException)
            {
                Trace.TraceError("Provided area is out of bounds! Capturing entire image. Fix this. Provided area is: " + searchArea.ToString());
                return img;
            }
        }

        private static Image<Rgba, byte> _CaptureBitmap()
        {
            var rc = WindowRectangle;
            var bitmap = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bitmap);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            Message.PrintWindow(WindowHWNDPtr, hdcBitmap, 0x1);
            gfxBmp.ReleaseHdc(hdcBitmap);
            Image.Dispose();
            Image = new Image<Rgba, byte>(bitmap);
            gfxBmp.Dispose();
            return Image;
        }
    }
}
