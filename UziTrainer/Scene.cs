﻿using System;
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
            for(uint i = 1; !ImageSearch.FindPoint(query, out coordinates); i++)
            {
                Thread.Sleep(500);
                if (stopwatch.ElapsedMilliseconds > 120000)
                {
                    Trace.WriteLine("Not found in 120s. Stopping.", query.ImagePath);
                    Program.TrainerThread.WaitOne();
                }
            }
            Trace.WriteLine(string.Format("Found [{0}]", query.ImagePath));
        }

        public static bool Exists(Query query)
        {
            return Exists(query, 3000, out _);
        }

        public static bool Exists(Query query, int timeout)
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
            Mouse.Click(coordinates.X, coordinates.Y, variance);
        }

        public static void Click(Query query, int varianceX=10, int varianceY=10)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(coordinates.X, coordinates.Y, varianceX, varianceY);
        }

        public static readonly Query HomeQuery = new Query("HomePage/" + Properties.Settings.Default.BaseBackground, new Rectangle(835, 571, 396, 95));
        public static readonly Query RepairQuery = new Query("RepairPage/RepairPage", new Rectangle(195, 73, 25, 25));
        public static readonly Query FormationQuery = new Query("FormationPage/FormationPage", new Rectangle(124, 82, 15, 6));
        public static readonly Query CombatQuery = new Query("CombatPage/CombatPage", new Rectangle(365, 65, 80, 50));
        public static readonly Query FactoryQuery = new Query("FactoryPage/FactoryPage", new Rectangle(56, 649, 40, 40));

        public static void WaitHome()
        {
            Scene.Wait(HomeQuery);
            Scene.Wait(new Query("HomePage/LV", new Rectangle(29, 45, 25, 20)));
            Thread.Sleep(2000);
        }

        public static void Transition(Query leave, Query enter, Point? click = null)
        {
            var sw = Stopwatch.StartNew();
            Wait(leave);
            while (true)
            {
                if (Exists(leave, 1500, out Point coords))
                {
                    Mouse.Click(click.HasValue ? (Point)click : coords);
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

        public static void ClickUntilFound(Query query, Point point)
        {
            do
            {
                Mouse.Click(point);
            } while (!Scene.Exists(query, 1000));
        }
    }
}
