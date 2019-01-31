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
            using (Window window = new Window(query.Area))
            {
                var stopwatch = Stopwatch.StartNew();
                while (!ImageSearch.FindPoint(window, query, out coordinates))
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
        }

        public static bool Exists(Query query)
        {
            return Exists(query, 5000, out _);
        }

        public static bool Exists(Query query, int timeout, out Point coordinates)
        {
            using (Window window = new Window(query.Area))
            {
                var stopwatch = Stopwatch.StartNew();
                while (!ImageSearch.FindPoint(window, query, out coordinates))
                {
                    Thread.Sleep(50);
                    if (stopwatch.ElapsedMilliseconds > timeout)
                    {
                        return false;
                    }
                }
                return true;
            }
        }        

        public static void Click(Query query)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(coordinates.X, coordinates.Y);
        }
    }
}
