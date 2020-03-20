using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer
{
    static class Screen
    {
        static readonly Random Random = new Random();

        public static bool Exists(Samples sample, int timeout = 0, bool debug = false)
        {
            return Exists(sample, timeout, out _, debug);
        }

        public static bool Exists(Samples sample, int timeout, out Tuple<Point, double> matchInfo, bool debug = false)
        {            
            var stopwatch = Stopwatch.StartNew();
            var template = Template.Get(sample);
            Trace.WriteLine($"Searching {template.Name}");

            Point point;
            Tuple<Point, double> rawMatchInfo;
            bool matched;
            do
            {
                matched = Match(Android.Screenshot().Copy(template.SearchArea), template.Image, out rawMatchInfo);
                if (debug)
                {
                    var form = new FormImageCreator(template);
                    form.ShowDialog();
                }
                if (matched) break;
                if (stopwatch.ElapsedMilliseconds >= timeout)
                {
                    Trace.WriteLine($"Not found {template.Name}");
                    point = new Point(rawMatchInfo.Item1.X + template.SearchArea.X, rawMatchInfo.Item1.Y + template.SearchArea.Y);
                    matchInfo = new Tuple<Point, double>(point, rawMatchInfo.Item2);
                    return false;
                }
                Thread.Sleep(200);
            }while(true);
            Trace.WriteLine($"Found {template.Name}");
            point = new Point(rawMatchInfo.Item1.X + template.SearchArea.X, rawMatchInfo.Item1.Y + template.SearchArea.Y);
            matchInfo = new Tuple<Point, double>(point, rawMatchInfo.Item2);
            return true;
        }
        public static void Wait(Samples sample)
        {
            Wait(Template.Get(sample), out _);
        }

        public static void Wait(Template template, out Tuple<Point, double> matchInfo, bool debug = false)
        {   
            var stopwatch = Stopwatch.StartNew();
            Trace.WriteLine($"Waiting for {template.Name}");
            bool matched;
            do
            {
                matched = Match(Android.Screenshot().Copy(template.SearchArea), template.Image, out matchInfo);
                if (debug)
                {
                    var form = new FormImageCreator(template);
                    form.ShowDialog();
                }
                if (matched) break;
                if (stopwatch.ElapsedMilliseconds > 120000)
                {
                    //Not found in 120s. Stopping.
                }
                Thread.Sleep(500);
            } while (true);
            Trace.WriteLine($"Found {template.Name}");
        }

        public static void Tap(Samples sample, bool debug = false)
        {   
            var template = Template.Get(sample);
            Wait(template, out var matchInfo, debug);

            if (template.ClickArea == Rectangle.Empty)
            {
                //matchInfo coordinates are relative to SearchArea, so fix to full image before tapping
                Android.Tap(matchInfo.Item1.X + template.SearchArea.X, matchInfo.Item1.Y + template.SearchArea.Y);
            }
            else
            {
                var area = template.ClickArea;
                var x = Random.Next(area.X, area.X + area.Width);
                var y = Random.Next(area.Y, area.Y + area.Height);
                Android.Tap(x, y, 0, 0);
            }
            do
            {
                if (template.Next == null)
                {
                    break;
                }
                else if (template.Next == Samples._Negative)
                {
                    if (!Exists(sample, 1000))
                    {
                        break;
                    }
                }
                else
                {
                    if (Exists(template.Next.Value, 5000))
                    {
                        break;
                    }
                }
            } while (true);
        }

        public static void Tap(int x, int y, Samples next, int rangeX = 10, int rangeY = 10)
        {
            do
            {
                Android.Tap(x, y, rangeX, rangeY);
                Thread.Sleep(1000);
            } while (!Exists(next));
        }

        static bool Match(Image<Rgba, byte> haystack, Image<Rgba, byte> needle, out Tuple<Point, double> matchInfo)
        {
            haystack.Save("c:/temp/out.png");
            using (Image<Gray, float> result = haystack.MatchTemplate(needle, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] maxValues;
                Point[] maxLocations;
                result.MinMax(out _, out maxValues, out _, out maxLocations);
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                matchInfo = new Tuple<Point, double>(maxLocations[0], maxValues[0]);
                return maxValues[0] > 0.85;
            }
        }
    }

    public enum Samples
    {
        _Negative,
        HomeCombat,
        HomeFormation,

        //Formation Screen
        FormationScreen,
        FormationExit,

        //Combat Screen
        CombatScreen,
        CombatMission,
        CombatMissionClicked,
        Chapter0,
        Chapter0Clicked,
        NormalBattle,
        M0_2,
        DollCapacity,
        EquipCapacity,

        //In mission
        Turn0,
        DeployOK,
        StartOperation,
        Resupply,
        EchelonFormation,
        PlanningOff,
        PlanningOn,
        ExecutePlan,
        EndRound,
        ReturnToBase,
        LowHP,
        EmergencyRepair,

        //Formation Screen
        FormationFilter,
        FormationFilterActive,
        Filter5Star,
        FilterAR,
        FilterConfirm,
        FilterReset,

        //Dolls
        HK416,
        SOPMOD
    }
}
