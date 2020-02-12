using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UziTrainer.Chapters;
using UziTrainer.Scenes;
using UziTrainer.Window;
using Screen = UziTrainer.Window.Screen;

namespace UziTrainer
{
    static class Program
    {
        public static AutoResetEvent DebugResetEvent = new AutoResetEvent(false);
        static Screen screen;
        public static FormMain form;
        public static FormDebug formDebug;
        public static int RunCounter = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PrepareAssets();            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new FormMain();
            Application.Run(form);
        }

        public static void IncreaseCounter()
        {
            RunCounter += 1;
            form.SetCounter(RunCounter);
        }

        public static void DecreaseCounter()
        {
            RunCounter -= 1;
            form.SetCounter(RunCounter);
        }

        public static void ResetCounter()
        {
            RunCounter = 0;
            form.SetCounter(RunCounter);
        }

        public static void Pause()
        {
            form.TogglePauseState();
        }

        public static void FlashTaskbar()
        {
            form.BeginInvoke((Action)(() => Win32.Message.FlashWindow(form.Handle, true)));            
        }

        public static void PrepareAssets()
        {
            if (Directory.Exists("./assets"))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead("./assets.zip"))
                    {
                        var hash = BitConverter.ToString(md5.ComputeHash(stream));
                        if (Properties.Settings.Default.AssetsHash == hash)
                        {
                            Trace.WriteLine("AssetsHash match, doing nothing");
                            return;
                        }
                        Properties.Settings.Default.AssetsHash = hash;
                        Properties.Settings.Default.Save();
                    }
                }
                Directory.Delete("./assets", true);
            }
            Trace.WriteLine("Extracting new assets");
            ZipFile.ExtractToDirectory("./assets.zip", "./");
        }

        public static void RunTest()
        {
            screen = new Screen(Properties.Settings.Default.WindowTitle)
            {
                Interruptible = false
            };
            //screen.Exists(Home.CombatButton, 0);
            var s = "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";
            var bytes = Convert.FromBase64String(s);
            using (var ms = new MemoryStream(bytes))
            {
                var bm = new Bitmap(ms);
                bm.Save("c:/temp/x.png");
            }

            /*
            Task.Run(() =>
            {
                var f = new FormImage("x", screen.CaptureScreen().Bitmap);
                f.Show();
                Application.Run(f);
            });            
            */
            //screen.CaptureScreen();
            //Console.WriteLine(screen.Exists(Combat.CombatMissionClicked, 0, true));
            //var maps = new Maps(screen);
            //maps.Drag0_2();
            //var f = new Formation(screen);
            //f.ReplaceCorpseDragger();

            Trace.WriteLine("Done");
        }

        public static void Run(string mission, int count)
        {
            ResetCounter();
            screen = new Screen(Properties.Settings.Default.WindowTitle);
            
            var repair = new Repair(screen);
            var formation = new Formation(screen);
            var combat = new Combat(screen);
            var factory = new Factory(screen);

            bool forcedEnhancement = false;
            for (int i=0; i!=count; i++)
            {
                screen.Interruptible = true;
                screen.Wait(Home.LvSample);
                Thread.Sleep(2000);
                if (screen.Exists(Home.CriticalDamaged))
                {
                    screen.Click(Home.RepairButton);
                    screen.Interruptible = false;
                    repair.RepairCritical();
                    screen.Interruptible = true;
                    screen.Click(Repair.ReturnToBase, Home.LvSample);

                }
                if (Properties.Settings.Default.IsCorpseDragging && !forcedEnhancement)
                {                    
                    screen.Click(Home.FormationButton);
                    screen.Interruptible = false;
                    formation.ReplaceCorpseDragger();
                    screen.Interruptible = true;
                    screen.Click(Formation.ReturnToBase, Home.LvSample);
                }
                screen.Click(Home.CombatButton);                
                while (true)
                {
                    combat.PrepareMission(mission);
                    var missionResult = combat.ExecuteMission(mission);
                    if (missionResult == MissionResult.EnhancementRequired)
                    {
                        forcedEnhancement = true;
                        Thread.Sleep(4000);
                        factory.DollEnhancement();
                        screen.Click(Factory.ReturnButton);
                        break;
                    }
                    if (missionResult == MissionResult.RetirementRequired)
                    {
                        break;
                    }
                    if (Properties.Settings.Default.IsCorpseDragging || missionResult == MissionResult.Finished)
                    {
                        forcedEnhancement = false;
                        screen.Click(Combat.ReturnToBase, Home.LvSample);
                        break;
                    }
                }
                IncreaseCounter();
            }
        }

        public static void ShowDebug(Sample sample, Point foundAt, float evaluation)
        {
            Task.Run(() =>
            {
                if (formDebug != null && !formDebug.IsDisposed)
                {
                    formDebug.Close();
                    formDebug.Dispose();
                }
                formDebug = new FormDebug(screen, sample, foundAt, evaluation);
                formDebug.Show();
                Application.Run(formDebug);
            });
        }

        internal static void LogisticsCheck()
        {
            ResetCounter();
            screen = new Screen(Properties.Settings.Default.WindowTitle);
            screen.Interruptible = true;
            var random = new Random();
            var refresh = random.Next(900000, 1800000);
            var stopwatch = Stopwatch.StartNew();
            while (true)
            {
                /*
                if (stopwatch.ElapsedMilliseconds > refresh)
                {
                    screen.Click(Home.FormationButton);
                    Thread.Sleep(850);
                    screen.Click(Formation.ReturnToBase);

                    stopwatch = Stopwatch.StartNew();
                    refresh = random.Next(900000, 1800000);
                }
                */
                if (screen.Exists(Home.LogisticsReturned, 1000))
                {
                    screen.SolveInterruptions();
                }
                if (screen.Exists(Home.ImportantInformation))
                {
                    Thread.Sleep(180000);
                    screen.Click(Home.ImportantInformation);
                    screen.Wait(Home.News);
                    screen.Click(Home.News);
                    screen.Wait(Home.Event);
                    screen.Click(Home.Event);
                }
                Thread.Sleep(20000);
            }
        }
    }
}
