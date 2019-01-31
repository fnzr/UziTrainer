using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace UziTrainer
{
    public class Window : IDisposable
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
        private Rectangle capturedArea;
        private Form drawForm = new Form();

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

        Rectangle searchArea;

        public Window(Rectangle searchArea)
        {
            this.searchArea = searchArea;
            RECT rc;
            Message.GetWindowRect(WindowHWNDPtr, out rc);
            capturedArea = rc;
            drawSearchArea();
        }

        public Bitmap CaptureBitmap()
        {
            return _CaptureBitmap();
        }

        public Bitmap CaptureBitmap(Rectangle searchArea)
        {   
            var bmp = _CaptureBitmap();
#if DEBUG
            var path = Path.Combine(Constants.DebugDir, DateTime.UtcNow.ToString("yyyyMMdd_HHmmssffffff") + ".png");
            bmp.Save(path, ImageFormat.Png);
            Tracer.TraceEvent(TraceEventType.Verbose, Constants.TraceDebug, "Captured [{0}]", path);
#endif
            return bmp.Clone(searchArea, bmp.PixelFormat);
        }

        private Bitmap _CaptureBitmap()
        {
            RECT rc;
            Message.GetWindowRect(WindowHWNDPtr, out rc);
            capturedArea = rc;

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            Message.PrintWindow(WindowHWNDPtr, hdcBitmap, 0x1);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bmp;
        }

        private void drawSearchArea()
        {
            drawForm = new Form();
            drawForm.BackColor = Color.Fuchsia;
            drawForm.ShowInTaskbar = false;
            drawForm.Opacity = .3f;
            drawForm.FormBorderStyle = FormBorderStyle.None;
            drawForm.StartPosition = FormStartPosition.Manual;
            drawForm.DesktopBounds = new Rectangle(capturedArea.Left + searchArea.Left, capturedArea.Top + searchArea.Top, searchArea.Width, searchArea.Height);
            drawForm.TopMost = true;
            var initialStyle = Message.GetWindowLongPtr(drawForm.Handle, -20).ToInt32();
            Message.SetWindowLongPtr(drawForm.Handle, -20, new IntPtr(initialStyle | 0x80000 | 0x20));
            drawForm.Show();
        }

        public void Dispose()
        {
            drawForm.Dispose();
        }
    }
}
