using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using UziTrainer.Scenes;

namespace UziTrainer.Window
{
    public class Screen
    {
        static Random random = new Random();

        readonly IntPtr RectangleHWND;
        bool resetTimer;
        private readonly System.Diagnostics.Process Process;
        public readonly Win32.Mouse mouse;
        public static readonly Rectangle FullArea = new Rectangle(0, 0, 1088, 816);
        //public static readonly Rectangle FullArea = new Rectangle(0, 0, 1048, 813);
        public bool Interruptible = true;

        Image<Rgba, byte> _Image = new Image<Rgba, byte>(1, 1);
        Image<Rgba, byte> _ImageLimited = new Image<Rgba, byte>(1, 1);

        public Screen(string windowTitle)
        {
            //var root = Win32.Message.FindWindow("MSPaintApp", "Untitled - Paint");
            //var mhwnd = Win32.Message.FindWindowEx(root, 0, "MSPaintView", "");
            //var y = Win32.Message.FindWindowEx(mhwnd, 0, "Afx:00007FF716220000:8", "");
            /*
            var root = Win32.Message.FindWindow("Qt5QWindowIcon", Properties.Settings.Default.WindowTitle);
            var mhwnd = Win32.Message.FindWindowEx(root, 0, "subWin", "sub");
            if (root <= 0 || mhwnd <= 0)
            {
                throw new ArgumentException("Could not find window handles");
            }
            RectangleHWND = new IntPtr(mhwnd);            
            
            mouse = new Win32.Mouse(root);
            */
            Process = new System.Diagnostics.Process();
            var info = new System.Diagnostics.ProcessStartInfo();
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            info.FileName = @"C:\Users\master\AppData\Local\Android\Sdk\platform-tools\adb.exe";
            info.Arguments = $"exec-out screencap -p";
            Process.StartInfo = info;
        }

        public int ExistsAny(Sample[] samples, bool debug = false)
        {
            var capture = CaptureScreen();
            for(int i=0; i<samples.Length; i++)
            {
                var sample = samples[i];
                var point = GetPoint(sample.Image, LimitSearchArea(capture, sample.SearchArea), sample.Threshold, sample, debug);
                if (point != Point.Empty)
                {
                    return i;
                }
            }            
            return -1;
        }
        
        public bool Exists(Sample sample, int timeout = 3000, bool debug = false)
        {
            Trace.WriteLine($"Searching for [{sample.Name}]");
            var stopwatch = Stopwatch.StartNew();
            Point found;
            while ((found = Search(sample, debug)) == Point.Empty)
            {
                Thread.Sleep(100);
                if (resetTimer)
                {
                    stopwatch = Stopwatch.StartNew();
                    resetTimer = false;
                }
                if (stopwatch.ElapsedMilliseconds > timeout)
                {
                    Trace.WriteLine($"Not found [{sample.Name}]");
                    return false;
                }
            }
            Trace.WriteLine($"Found [{sample.Name}]");
            return true;
        }

        public void Click(Rectangle area, bool debug)
        {
            Click(area, null, 0, debug);
        }

        public void Click(Rectangle area, Sample next = null, int wait = 3000, bool debug = false)
        {
            if (debug)
            {
                var sample = new Sample("", area);
                CreateDebugForm(sample, Point.Empty, 0f);
            }
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
            do
            {
                if (button.Next == null)
                {
                    break;
                }
                else if (button.Next == Sample.Negative)
                {
                    if (!Exists(button, 1000))
                    {
                        break;
                    }
                }
                else
                {
                    if (Exists(button.Next, 5000))
                    {
                        break;
                    }
                }
                Thread.Sleep(2000);
                if (Exists(button, 3000))
                {
                    foundAt = Wait(button);
                    area = button.ClickArea(button.AbsolutePosition(foundAt));
                    x = area.X + random.Next(0, area.Width);
                    y = area.Y + random.Next(0, area.Height);
                    mouse.Click(x, y);
                }
            } while (true);
        }

        

        public Point Wait(Sample sample, bool debug = false)
        {
            Trace.WriteLine($"Waiting for [{sample.Name}]");
            var stopwatch = Stopwatch.StartNew();
            Point found;
            while ((found = Search(sample, debug)) == Point.Empty)
            {
                Thread.Sleep(100);
                if (stopwatch.ElapsedMilliseconds > 120000)
                {
                    Trace.WriteLine($"Not found [{sample.Name}] in 120s. Stopping.");
                    Program.Pause();
                    stopwatch = Stopwatch.StartNew();

                }
            }
            Trace.WriteLine($"Found [{sample.Name}]");
            return found;
        }

        Point Search(Sample sample, bool debug)
        {            
            Point foundAt = Point.Empty;
            var capture = CaptureScreen();
            if (Interruptible)
            {
                var copy = LimitSearchArea(capture, Home.LogisticsReturned.SearchArea);
                if (GetPoint(Home.LogisticsReturned.Image, copy, Home.LogisticsReturned.Threshold) != Point.Empty)
                {
                    SolveInterruptions();
                    resetTimer = true;
                    capture = CaptureScreen();
                }
            }            
            return GetPoint(sample.Image, LimitSearchArea(capture, sample.SearchArea), sample.Threshold, sample, debug);
        }

        Point GetPoint(Image<Rgba, byte> needle, Image<Rgba, byte> haystack, float threshold, Sample sample = null, bool debug = false)
        {
            Point foundAt;
            using (Image<Gray, float> result = haystack.MatchTemplate(needle, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] maxValues;
                Point[] maxLocations;
                result.MinMax(out _, out maxValues, out _, out maxLocations);
                //Console.WriteLine(maxValues[0]);
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] >= threshold)
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
            Win32.Message.GetWindowRect(RectangleHWND, out rc);
            return rc;
        }

        public void SolveInterruptions()
        {
            Interruptible = false;
            bool found;
            var stopwatch = Stopwatch.StartNew();
            do
            {
                Click(new Rectangle(705, 120, 300, 640));
                found = Exists(Home.LogisticsRepeatButton);
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    break;
                }
                Thread.Sleep(500);
            } while (!found);
            if (found)
            {
                Click(Home.LogisticsRepeatButton);
            }
            Thread.Sleep(2000);
            Interruptible = true;
        }

        public Image<Rgba, byte> CaptureScreen()
        {
            return new Image<Rgba, byte>(new System.Drawing.Bitmap(@"C:\temp\screen.png"));
            /*
            Process.Start();
            FileStream baseStream = Process.StandardOutput.BaseStream as FileStream;            

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[4096];
                int lastRead = 0;
                do
                {
                    lastRead = baseStream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, lastRead);
                } while (lastRead > 0);
                _Image.Dispose();
                _Image = new Image<Rgba, byte>(new Bitmap(ms));
                _Image.Save(@"C:\temp\out.png");
            }
            return _Image;
            */
        }

        Image<Rgba, byte> LimitSearchArea(Image<Rgba, byte> image, Rectangle area)
        {
            _ImageLimited.Dispose();
            _ImageLimited = image.Copy(area);
            //_ImageLimited.Save(@"C:\temp\out2.png");
            return _ImageLimited;
        }
    }
}
