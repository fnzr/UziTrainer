using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer
{
    static class Android
    {
        static readonly string ADBPath = Properties.Settings.Default.ADB_Path;
        static readonly Random Random = new Random();
        static Android()
        {
            Connect();
        }

        private static void Connect()
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = ADBPath,
                    //MeMu port
                    Arguments = $"connect localhost:21503"
                }
            };
            proc.Start();
            proc.WaitForExit();
        }

        public static Image<Rgba, byte> Screenshot()
        {
            var proc = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = ADBPath,
                    Arguments = $"exec-out screencap -p"
                }
            };
            proc.Start();

            FileStream baseStream = proc.StandardOutput.BaseStream as FileStream;
            Image<Rgba, byte> image;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[4096];
                int lastRead = 0;
                do
                {
                    lastRead = baseStream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, lastRead);
                } while (lastRead > 0);
                image = new Image<Rgba, byte>(new Bitmap(ms));
            }
            return image;
        }

        public static void Tap(int x, int y, int rangeX = 10, int rangeY = 10)
        {
            var a = Random.Next(x - rangeX, x + rangeX);
            var b = Random.Next(y - rangeY, y + rangeY);
            Trace.TraceInformation($"Tapping ({a}, {b})");
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = ADBPath,
                    Arguments = $"shell input tap {a} {b}"
                }
            };            
            process.Start();
            process.WaitForExit();

            Thread.Sleep(Random.Next(1000, 3000));
        }
    }
}
