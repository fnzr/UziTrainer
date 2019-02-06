using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using UziTrainer.Page;
using UziTrainer.Scenes;

namespace UziTrainer
{    
    static class Program
    {
        public static ManualResetEvent TrainerThread = new ManualResetEvent(false);
        //public static Thread ExecutionThread;
        public static FormMain main;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            PrepareResources();
            Window.Init();
            main = new FormMain();
            Application.EnableVisualStyles();
            Application.Run(main);
            Application.Exit();
        }

        public static void Pause()
        {
            main.PauseExecution();
            TrainerThread.WaitOne();
        }

        public static void PrepareResources()
        {
            if(Directory.Exists("./assets"))
            {
                using(var md5 = MD5.Create())
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

            Scene.WaitHome();
            if (Scene.Exists(new Query("CriticalRepair", new Rectangle(1007, 264, 25, 25))))
            {
                Trace.WriteLine("Performing Repairs");
                Scene.Transition(Scene.HomeQuery, Scene.RepairQuery, new Point(938, 302));
                Repair.RepairCritical();
            }
            if (SwapDoll.Default.Active)
            {
                Trace.WriteLine("Preparing formation");
                Scene.Transition(Scene.HomeQuery, Scene.FormationQuery, new Point(1161, 476));
                Formation.SetDragFormation();
            }
            Scene.Transition(Scene.HomeQuery, Scene.CombatQuery, new Point(930, 500));
            Combat.Setup(Properties.Settings.Default.SelectedMission);
        }
    }

}
