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
            var maps = new Maps(screen);
            maps.CT_Scarecrow();
            //maps.CT_PythonFarm();
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
