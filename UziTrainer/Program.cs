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

        public static void Run()
        {
            screen = new Screen("ZR288");            
            //screen.mouse.Click(42, 279);
            //formation.ReplaceCorpseDragger();
            
            var repair = new Repair(screen);
            var formation = new Formation(screen);
            var combat = new Combat(screen);
            bool forcedEnhancement = false;
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
                forcedEnhancement = false;
                screen.Click(Home.FormationButton);
                screen.Interruptible = false;
                formation.ReplaceCorpseDragger();
                screen.Interruptible = true;
                screen.Click(Formation.ReturnToBase, Home.LvSample);
            }
            screen.Click(Home.CombatButton);
            combat.PrepareMission("0_2");
            while (true)
            {
                var missionResult = combat.ExecuteMission("0_2");
                if (missionResult == MissionResult.EnhancementRequired)
                {
                    forcedEnhancement = true;
                    break;
                }
                if (missionResult == MissionResult.RetirementRequired)
                {
                    break;
                }
                if (Properties.Settings.Default.IsCorpseDragging)
                {
                    screen.Click(Combat.ReturnButton);
                    break;
                }
            }
            //var combat = new Combat(screen);
            //combat.PrepareMission("0_2");
            //var c0 = new Chapter0(screen, "0_2");
            //c0.Map0_2();

            //screen.Wait(Chapter.EchelonFormationButton, true);
            //screen.Wait(Chapter.DeployEchelonButton, true);
            //var formation = new Formation(screen);
            //Combat.MissionButton.Name = "CombatPage/1_6";
            //screen.Click(Home.CombatButton, true);
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
