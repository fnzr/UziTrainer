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
                    Trace.WriteLine("Could not find [{0}] in 120s. Stopping.", query.ImagePath);
                    Program.TrainerThread.WaitOne();
                }
            }
            Trace.WriteLine(string.Format("Found [{0}]", query.ImagePath));
        }

        public static bool Exists(Query query)
        {
            return Exists(query, 3000, out _);
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

        public static void Click(Query query)
        {
            Wait(query, out Point coordinates);
            Mouse.Click(coordinates.X, coordinates.Y);
        }

        public static readonly Query HomeQuery = new Query("HomePage/" + Properties.Settings.Default.BaseBackground, new Rectangle(835, 571, 396, 95));
        public static readonly Query RepairQuery = new Query("RepairPage/RepairPage", new Rectangle(195, 73, 25, 25));
        public static readonly Query FormationQuery = new Query("FormationPage/FormationPage", new Rectangle(124, 82, 15, 6));

        public static void WaitHome()
        {
            Scene.Wait(HomeQuery);
            Scene.Wait(new Query("HomePage/LV", new Rectangle(29, 45, 25, 20), true));
            Thread.Sleep(2000);
        }

        public static void Transition(Query leave, Query enter, Point? click = null)
        {
            var sw = Stopwatch.StartNew();
            Wait(leave);
            while (true)
            {
                Point coords;
                if (Exists(leave, out coords))
                {
                    coords = click.HasValue ? (Point)click : coords;
                    Mouse.Click(coords.X, coords.Y);
                }
                if (Exists(enter, 2000, out _))
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
    }
}
