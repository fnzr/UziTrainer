using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace UziTrainer
{
    static class Screen
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
                    Trace.WriteLine("Not found in 120s. Stopping.", query.ImagePath);
                    Program.Pause();
                }
            }
            Trace.WriteLine(string.Format("Found [{0}]", query.ImagePath));
        }

        private static bool CheckInterruptions()
        {
            /*
            Interruptible = false;
            if (Exists(new Query("LogisticsReturned", new Rectangle(1174, 87, 30, 30)), 500))
            {
                Program.WriteLog("Logistics Returned");
                Transition(new Query("LogisticsReturned", new Rectangle(1174, 87, 30, 30)),
                    new Query("LogisticsConfirm"), new RPoint(754, 524));
                Mouse.Click(741, 522);
                return true;
            }
            else
            {
                //TODO AutoBattle
            }
            Interruptible = true;
            */
            return false;
        
        }

        public static bool Exists(Query query, int timeout = 3000)
        {
            return Exists(query, timeout, out _);
        }

        public static bool Exists(Query query, out Point coordinates)
        {
            return Exists(query, 3000, out coordinates);
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

        public static void Click(Query query, int variance)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(new RPoint(coordinates.X, coordinates.Y, variance));
        }

        public static void Click(Query query, int varianceX = 10, int varianceY = 10)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(coordinates.X, coordinates.Y, varianceX, varianceY);
        }

        public static void Click(RPoint point)
        {
            Mouse.Click(point);
        }

        public static void Click(Point point, int variance=10)
        {
            Mouse.Click(point.X, point.Y,variance, variance);
        }

        public static void Click(Point point, int varianceX, int varianceY)
        {
            Mouse.Click(point.X, point.Y, varianceX, varianceY);
        }        

        public static readonly Query HomeQuery = new Query("HomePage/" + Properties.Settings.Default.BaseBackground, new Rectangle(835, 571, 396, 95));
        public static readonly Query RepairQuery = new Query("RepairPage/RepairPage", new Rectangle(195, 73, 25, 25));
        public static readonly Query FormationQuery = new Query("FormationPage/FormationPage", new Rectangle(124, 82, 15, 6));
        public static readonly Query CombatQuery = new Query("CombatPage/CombatPage", new Rectangle(365, 65, 80, 50));
        public static readonly Query FactoryQuery = new Query("FactoryPage/FactoryPage", new Rectangle(56, 649, 40, 40));

        public static void WaitHome()
        {
            Wait(HomeQuery);
            Wait(new Query("HomePage/LV", new Rectangle(29, 45, 25, 20)));
            Thread.Sleep(2000);
        }

        public static void Transition(Query leave, Query enter, RPoint? click = null)
        {
            var sw = Stopwatch.StartNew();
            Wait(leave);
            while (true)
            {
                if (Exists(leave, 1500, out Point coords))
                {
                    if (click.HasValue)
                    {
                        Mouse.Click((RPoint)click);
                    }
                    else
                    {
                        Mouse.Click(coords);
                    }
                    
                }
                if (Exists(enter, 2000))
                {
                    break;
                }
                if (sw.Elapsed.Milliseconds > 120000)
                {
                    Trace.WriteLine(string.Format("Could not perform transition [{0}] => [{1}]. Stopping.", leave.ImagePath, enter.ImagePath));
                    Program.Pause();
                }
            }
        }

        public static void ClickUntilFound(Query query, RPoint point)
        {
            do
            {
                Mouse.Click(point);
            } while (!Exists(query, 1000));
        }

        public static void ClickUntilGone(Query query, RPoint? point)
        {
            do
            {
                Mouse.Click(point);
            } while (Exists(query, 1000));
        }
    }

    public readonly struct RPoint
    {
        public readonly int X;
        public readonly int Y;
        public readonly int VarianceX;
        public readonly int VarianceY;

        public RPoint(int x, int y, int variance=10)
        {
            X = x;
            Y = y;
            VarianceX = variance;
            VarianceY = variance;
        }

        public RPoint(int x, int y, int varianceX, int varianceY)
        {
            X = x;
            Y = y;
            VarianceX = varianceX;
            VarianceY = varianceY;
        }

        public RPoint(Point p, int varianceX, int varianceY)
        {
            X = p.X;
            Y = p.Y;
            VarianceX = varianceX;
            VarianceY = varianceY;
        }
    }
}
