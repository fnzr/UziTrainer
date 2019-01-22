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

        public static Point FindBitmapsEntry(Bitmap sourceBitmap, Bitmap serchingBitmap)
        {
            #region Arguments check

            if (sourceBitmap == null || serchingBitmap == null)
                throw new ArgumentNullException();
            Console.WriteLine(sourceBitmap.PixelFormat);
            Console.WriteLine(serchingBitmap.PixelFormat);
            if (sourceBitmap.PixelFormat != serchingBitmap.PixelFormat)
                throw new ArgumentException("Pixel formats arn't equal");

            if (sourceBitmap.Width < serchingBitmap.Width || sourceBitmap.Height < serchingBitmap.Height)
                throw new ArgumentException("Size of serchingBitmap bigger then sourceBitmap");

            #endregion

            var pixelFormatSize = Image.GetPixelFormatSize(sourceBitmap.PixelFormat) / 8;


            // Copy sourceBitmap to byte array
            var sourceBitmapData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, sourceBitmap.PixelFormat);
            var sourceBitmapBytesLength = sourceBitmapData.Stride * sourceBitmap.Height;
            var sourceBytes = new byte[sourceBitmapBytesLength];
            Marshal.Copy(sourceBitmapData.Scan0, sourceBytes, 0, sourceBitmapBytesLength);
            sourceBitmap.UnlockBits(sourceBitmapData);

            // Copy serchingBitmap to byte array
            var serchingBitmapData =
                serchingBitmap.LockBits(new Rectangle(0, 0, serchingBitmap.Width, serchingBitmap.Height),
                    ImageLockMode.ReadOnly, serchingBitmap.PixelFormat);
            var serchingBitmapBytesLength = serchingBitmapData.Stride * serchingBitmap.Height;
            var serchingBytes = new byte[serchingBitmapBytesLength];
            Marshal.Copy(serchingBitmapData.Scan0, serchingBytes, 0, serchingBitmapBytesLength);
            serchingBitmap.UnlockBits(serchingBitmapData);

            var pointsList = new List<Point>();

            // Serching entries
            // minimazing serching zone
            // sourceBitmap.Height - serchingBitmap.Height + 1
            for (var mainY = 0; mainY < sourceBitmap.Height - serchingBitmap.Height + 1; mainY++)
            {
                var sourceY = mainY * sourceBitmapData.Stride;

                for (var mainX = 0; mainX < sourceBitmap.Width - serchingBitmap.Width + 1; mainX++)
                {// mainY & mainX - pixel coordinates of sourceBitmap
                 // sourceY + sourceX = pointer in array sourceBitmap bytes
                    var sourceX = mainX * pixelFormatSize;

                    var isEqual = true;
                    for (var c = 0; c < pixelFormatSize; c++)
                    {// through the bytes in pixel
                        if (sourceBytes[sourceX + sourceY + c] == serchingBytes[c])
                            continue;
                        isEqual = false;
                        break;
                    }

                    if (!isEqual) continue;

                    var isStop = false;

                    // find fist equalation and now we go deeper) 
                    for (var secY = 0; secY < serchingBitmap.Height; secY++)
                    {
                        var serchY = secY * serchingBitmapData.Stride;

                        var sourceSecY = (mainY + secY) * sourceBitmapData.Stride;

                        for (var secX = 0; secX < serchingBitmap.Width; secX++)
                        {// secX & secY - coordinates of serchingBitmap
                         // serchX + serchY = pointer in array serchingBitmap bytes

                            var serchX = secX * pixelFormatSize;

                            var sourceSecX = (mainX + secX) * pixelFormatSize;

                            for (var c = 0; c < pixelFormatSize; c++)
                            {// through the bytes in pixel
                                if (sourceBytes[sourceSecX + sourceSecY + c] == serchingBytes[serchX + serchY + c]) continue;

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
                        return  new Point(mainX, mainY);
                    }
                }
            }
            throw new InvalidDataException("[{}] not found!");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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

            var source = new Bitmap(@"C:\Users\master\Nox_share\ImageShare\Screenshot_2019-01-22-16-39-58.png");
            var search = new Bitmap(@"C:\Users\master\Pictures\Screenshot_3.png");
            Console.WriteLine(FindBitmapsEntry(bmp, search));
            return;
            Stopwatch sw = Stopwatch.StartNew();
            for(var i=0; i<0; i++)
            {
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
                */
            }
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
