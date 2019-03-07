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
        static FormMain form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PrepareAssets();            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Run();
            form = new FormMain();
            Application.Run(form);
            
        }

        public static void Pause()
        {
            form.PauseExecution();
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
            screen = new Screen(Properties.Settings.Default.NoxTitle);
            screen.Interruptible = false;

            //Combat.SanityCheck.Name = "Missions/4_6/SanityCheck";
            //MessageBox.Show(screen.Exists(Combat.SanityCheck).ToString());
            //return;
            var c = new Chapter5(screen, "5_6");
            c.Map5_6();
            //Size nodeSize = new Size(50, 35);
            //var plan2 = new UziTrainer.Window.Button("Missions/0_2/Plan2", new Rectangle(479, 607, 45, 37), null);
            //screen.Click(plan2, true);
            //var r = new Factory(screen);
            //r.RepairCritical();
            //f.SelectEnhaceable();
            //r.DollEnhancement();
        }

        public static void Run(string mission, int count)
        {
            screen = new Screen(Properties.Settings.Default.NoxTitle);
            //screen.mouse.Click(42, 279);
            //formation.ReplaceCorpseDragger();
            
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
                form.IncreaseCounter();
            }
        }

        public static void ShowDebug(Sample sample, Point foundAt, float evaluation)
        {
            Task.Run(() =>
            {
                var form = new FormDebug(screen, sample, foundAt, evaluation);
                form.Show();
                Application.Run(form);
            });
        }
    }
}
