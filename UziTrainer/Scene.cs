using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace UziTrainer
{
    static class Scene
    {
        public static void Wait(Query query)
        {
            Wait(query, out _);
        }

        public static void Wait(Query query, out Point coordinates)
        {
            Trace.WriteLine(string.Format("Waiting for [{0}]", query.ImagePath));
            var stopwatch = Stopwatch.StartNew();
            while (!ImageSearch.FindPoint(query, out coordinates))
            {
                Thread.Sleep(500);
                if (stopwatch.ElapsedMilliseconds > 120000)
                {
                    Trace.WriteLine("Could not find [{0}] in 120s. Stopping.", query.ImagePath);
                    Program.TrainerThread.WaitOne();
                }
            }
            Trace.WriteLine(string.Format("Found [{0}]", query.ImagePath));
        }

        public static bool Exists(Query query)
        {
            return Exists(query, 5000, out _);
        }

        public static bool Exists(Query query, int timeout, out Point coordinates)
        {
            Trace.WriteLine(string.Format("Searching for [{0}]", query.ImagePath));
            var stopwatch = Stopwatch.StartNew();
            while (!ImageSearch.FindPoint(query, out coordinates))
            {
                Thread.Sleep(50);
                if (stopwatch.ElapsedMilliseconds > timeout)
                {
                    Trace.WriteLine(string.Format("Not found [{0}]", query.ImagePath));
                    return false;
                }
            }
            Trace.WriteLine(string.Format("Found [{0}]", query.ImagePath));
            return true;
        }        

        public static void Click(Query query)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(coordinates.X, coordinates.Y);
        }
    }
}
