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
        private static FormMain formMain;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            PrepareResources();
            Window.Init();
            formMain = new FormMain();

            Application.EnableVisualStyles();
            Application.Run(formMain);
            Application.Exit();
        }

        public static void Pause()
        {
            formMain.PauseExecution();
        }

        public static void WriteLog(string message)
        {
            formMain.WriteLog(message);
        }

        public static void UpdateDollText()
        {
            formMain.UpdateDollText();
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

        }

        /*
        public static void Run(Screen scene)
        {            
            scene.Interruptible = true;
            scene.WaitHome();
            if (scene.Exists(new Query("CriticalRepair", new Rectangle(1007, 264, 25, 25))))
            {
                var repair = new Repair(scene);
                Trace.WriteLine("Performing Repairs");
                scene.Transition(Screen.HomeQuery, Screen.RepairQuery, new Point(938, 302));
                scene.Interruptible = false;
                repair.RepairCritical();
                scene.Interruptible = true;
            }
            if (SwapDoll.Default.Active)
            {
                Trace.WriteLine("Preparing formation");
                scene.Transition(Screen.HomeQuery, Screen.FormationQuery, new Point(1161, 476));
                scene.Interruptible = false;
                var formation = new Formation(scene);
                formation.SetDragFormation();
                scene.Interruptible = true;
            }
            scene.Transition(Screen.HomeQuery, Screen.CombatQuery, new Point(930, 500));
            var combat = new Combat(scene);
            combat.Setup(Properties.Settings.Default.SelectedMission);
        }
        */
    }

}
