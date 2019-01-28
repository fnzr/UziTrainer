using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer
{
    static class Scene
    {
        private readonly static TraceSource Tracer = new TraceSource("fnzr.UziTrainer");
        public static void Wait(Query query)
        {

        }

        public static void Wait(Query query, out Point coordinates)
        {
            var stopwatch = Stopwatch.StartNew();
            while (!ImageSearch.FindPoint(query.Image, query.Tolerance, out coordinates)) {
                Thread.Sleep(500);
                if (stopwatch.ElapsedMilliseconds > 120000)
                {
                    Tracer.TraceInformation("Could not find [{0}] in 120s. Stopping.", query.ImagePath);
                    Program.TrainerThread.WaitOne();
                }
            }
            Tracer.TraceInformation("Found [{0}]", query.ImagePath);
        }

        public static bool Exists(Query query)
        {
            return Exists(query, 5000, out _);
        }

        public static bool Exists(Query query, int timeout, out Point coordinates)
        {
            var stopwatch = Stopwatch.StartNew();
            while (!ImageSearch.FindPoint(query.Image, query.Tolerance, out coordinates))
            {
                Thread.Sleep(50);
                if (stopwatch.ElapsedMilliseconds > timeout)
                {
                    return false;
                }
            }
            return true;
        }        

        public static void Click(Query query)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(coordinates.X, coordinates.Y);
        }
    }
}
