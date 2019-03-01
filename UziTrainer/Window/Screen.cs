using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using UziTrainer.Scenes;

namespace UziTrainer.Window
{
    public class Screen
    {
        const string MessageWindowClass = "Qt5QWindowIcon";
        const string MessageWindowTitle = "ScreenBoardClassWindow";
        const string ScreenWindowClass = "Qt5QWindowIcon";

        static Random random = new Random();

        readonly IntPtr WindowHWND;
        readonly IntPtr MessageHWND;
        public readonly Win32.Mouse mouse;
        public static readonly Rectangle FullArea = new Rectangle(0, 0, 1284, 722);
        public bool Interruptible = true;

        Image<Rgba, byte> _Image = new Image<Rgba, byte>(1, 1);

        public Screen(string windowTitle)
        {
            var hwnd = Win32.Message.FindWindow(ScreenWindowClass, windowTitle);
            if (hwnd <= 0)
            {
                throw new ArgumentException($"No HWND for window [{windowTitle}]");
            }
            var mhwnd = Win32.Message.FindWindowEx(hwnd, 0, MessageWindowClass, MessageWindowTitle);
            WindowHWND = new IntPtr(hwnd);
            MessageHWND = new IntPtr(mhwnd);
            mouse = new Win32.Mouse(hwnd, mhwnd);            
        }

        public bool Exists(Sample sample, int timeout = 3000, bool debug = false)
        {
            Trace.WriteLine($"Searching for [{sample.Name}]");
            var stopwatch = Stopwatch.StartNew();
            Point found;
            while ((found = Search(sample, debug)) == Point.Empty)
            {
                Thread.Sleep(500);
                if (stopwatch.ElapsedMilliseconds > timeout)
                {
                    Trace.WriteLine($"Not found [{sample.Name}]");
                    return false;
                }
            }
            Trace.WriteLine($"Found [{sample.Name}]");
            return true;
        }

        public void Click(Rectangle area, Sample next = null, int wait = 3000)
        {
            do
            {
                var x = area.X + random.Next(0, area.Width);
                var y = area.Y + random.Next(0, area.Height);
                mouse.Click(x, y);
            } while (next != null && !Exists(next, wait));

            var sleep = random.Next(400, 800);
            Thread.Sleep(sleep);
        }

        public void Click(Button button, bool debug = false)
        {
            var foundAt = Wait(button, debug);
            var area = button.ClickArea(button.AbsolutePosition(foundAt));
            var x = area.X + random.Next(0, area.Width);
            var y = area.Y + random.Next(0, area.Height);
            mouse.Click(x, y);
            if (button.Next == null)
            {
                return;
            }
            else if (button.Next == Sample.Negative)
            {
                while (Exists(button))
                {
                }
            }
            else
            {
                Wait(button.Next);
            }
        }

        public Point Wait(Sample sample, bool debug = false)
        {
            Trace.WriteLine($"Waiting for [{sample.Name}]");
            var stopwatch = Stopwatch.StartNew();
            Point found;
            while ((found = Search(sample, debug)) == Point.Empty)
            {
                Thread.Sleep(500);
                if (stopwatch.ElapsedMilliseconds > 120000)
                {
                    Trace.WriteLine($"Not found [{sample.Name}] in 120s. Stopping.");
                    //TODO pause
                }
            }
            Trace.WriteLine($"Found [{sample.Name}]");
            return found;
        }

        Point Search(Sample sample, bool debug)
        {
            if (Interruptible)
            {
                SolveInterruptions();
            }
            Point foundAt = Point.Empty;
            using (Image<Gray, float> result = CaptureScreen(sample.SearchArea).MatchTemplate(sample.Image, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] maxValues;
                Point[] maxLocations;
                result.MinMax(out _, out maxValues, out _, out maxLocations);
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] >= sample.Threshold)
                {
                    foundAt = maxLocations[0];
                }
                else
                {
                    foundAt = Point.Empty;
                }
                if (debug)
                {
                    CreateDebugForm(sample, foundAt, (float)maxValues[0]);
                }
            }

            return foundAt;
        }

        void CreateDebugForm(Sample sample, Point point, float evaluation)
        {
            Program.ShowDebug(sample, point, evaluation);
            Program.DebugResetEvent.WaitOne();
        }

        public Rectangle ReferenceRectangle()
        {
            Win32.RECT rc;
            Win32.Message.GetWindowRect(WindowHWND, out rc);
            return rc;
        }

        void SolveInterruptions()
        {
            Interruptible = false;
            if (Exists(Home.LogisticsReturned, 0))
            {
                Click(new Rectangle(1049, 580, 50, 50));
                Click(Home.LogisticsRepeatButton);
                Thread.Sleep(500);
            }
            Interruptible = true;
        }

        Image<Rgba, byte> CaptureScreen(Rectangle searchArea)
        {
            var rc = ReferenceRectangle();
            var bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);            
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            Win32.Message.PrintWindow(WindowHWND, hdcBitmap, 0x1);            
            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            var bitmap = new Bitmap(searchArea.Width, searchArea.Height, PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bitmap);
            var section = new Rectangle(searchArea.X, searchArea.Y, searchArea.Width, searchArea.Height);
            g.DrawImage(bmp, 0, 0, section, GraphicsUnit.Pixel);

            bmp.Dispose();            
            _Image.Dispose();
            _Image = new Image<Rgba, byte>(bitmap);
            return _Image;
        }
    }
}
