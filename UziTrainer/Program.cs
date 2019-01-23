using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UziTrainer
{
    static class Program
    {
        static int GetLParam(int x, int y)
        {
            return (x & 0xFFFF) | (y & 0xFFFF) << 16;
        }

        static int CompareColours(Color x, Color y)
        {
            return (int)(Math.Pow((int)x.R - y.R, 2) + Math.Pow((int)x.B - y.B, 2) + Math.Pow((int)x.G - y.G, 2));
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var c = "Qt5QWindowIcon";
            var window = new Window(c, "NoxPlayer", c, "ScreenBoardClassWindow");            
            var r = window.ImageExists(@"C:\Users\master\Pictures\Screenshot_3.png");
            Console.WriteLine(r);
            /*
            var source = new Bitmap(@"C:\Users\master\Nox_share\ImageShare\Screenshot_2019-01-22-16-39-58.png");
            var search = new Bitmap(@"C:\Users\master\Pictures\Screenshot_3.png");
            Console.WriteLine(FindBitmapsEntry(bmp, search));
            */
            //return;
            Console.WriteLine("=========");
            Stopwatch sw = Stopwatch.StartNew();
            //for(var i=0; i<0; i++)
           // {
               /* 
                var hwndNox = Win32.FindWindow("Qt5QWindowIcon", "NoxPlayer");
                var hwnd = Win32.FindWindowEx(hwndNox, 0, "Qt5QWindowIcon", "NoxPlayer");
                var hwndPtr = new IntPtr(hwndNox);
                RECT rc;
                Win32.GetWindowRect(hwndPtr, out rc);

                Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
                Graphics gfxBmp = Graphics.FromImage(bmp);
                IntPtr hdcBitmap = gfxBmp.GetHdc();

                Win32.PrintWindow(hwndPtr, hdcBitmap, 0);

                gfxBmp.ReleaseHdc(hdcBitmap);
                gfxBmp.Dispose();
            */
            /*
            using(FileStream fs = new FileStream(Path.GetTempFileName(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.RandomAccess | FileOptions.DeleteOnClose))
            {                    
                bmp.Save(fs, ImageFormat.Png);
            }

        //}
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
        return;

        var Form1 = new Form1();
        Graphics graphicDC;
        Bitmap memBmp;
        memBmp = new Bitmap(1280, 720);

        Graphics clientDC = Form1.CreateGraphics();
        IntPtr hdc = clientDC.GetHdc();
        IntPtr memdc = Win32Support.CreateCompatibleDC(hdc);
        Win32Support.SelectObject(memdc, memBmp.GetHbitmap());
        // Win32.SendMessage(hwnd, 0x0317, hdc.ToInt32(), 4);
        // Win32.PrintWindow(new IntPtr(hwnd), memdc, 0);
        graphicDC = Graphics.FromHdc(memdc);
        // Console.WriteLine(hwnd);
        Bitmap bitmap = new Bitmap(Convert.ToInt32(1280), Convert.ToInt32(720));
        graphicDC.DrawImage(bitmap, 0, 0);
        // memDC.DrawImage(bitmap, 0, 0);
        bitmap.Save(@"C:\temp\test.png", ImageFormat.Png);

        clientDC.ReleaseHdc(hdc);

        /*
        var hwndNox = Win32.FindWindow("Qt5QWindowIcon", "NoxPlayer");
        var hwndTitleBar = Win32.FindWindow("Qt5QWindowIcon", "default_title_barWindow");
        Console.WriteLine(hwndNox);
        Console.WriteLine(hwndTitleBar);
        var hwnd = Win32.FindWindowEx(hwndNox, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow");
        var coords = GetLParam(923, 495);
        Console.WriteLine(coords);
        Win32.PostMessage(hwnd, Win32.WM_LBUTTONDOWN, 0x0001, coords);
        Win32.PostMessage(hwnd, Win32.WM_LBUTTONUP, 0x0, coords);
        Console.WriteLine(hwnd);
        */
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
