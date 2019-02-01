using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UziTrainer.Scenes;

namespace UziTrainer
{
    static class Program
    {
        public static ManualResetEvent TrainerThread = new ManualResetEvent(false);
        public static Thread ExecutionThread;
        public static FormMain main;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Window.Init();
            main = new FormMain();
            //Scene.Wait(new Query("HomePage/LV", new Rectangle(0,0, 100, 100), true));
            //var Formation = new Formation();
            //Formation.ReplaceDoll(Doll.Get("SVD"), Doll.Get("WA2000"));
            Application.EnableVisualStyles();
            Application.Run(main);
        }

        public static void Pause()
        {
            main.SetPausedInfo();
            TrainerThread.WaitOne();
        }

        public static void Run()
        {
            var formation = new Formation();
            Scene.WaitHome();
            if (Scene.Exists(new Query("CriticalRepair", new Rectangle(1007, 264, 25, 25))))
            {
                Trace.WriteLine("Performing Repairs");
                Scene.Transition(Scene.HomeQuery, Scene.RepairQuery, new Point(938, 302));
                //Repair.RepairCritical();
            }
            if (SwapDoll.Default.Active)
            {
                Trace.WriteLine("Preparing formation");
                Scene.Transition(Scene.HomeQuery, Scene.FormationQuery, new Point(1161, 476));
                formation.SetDragFormation();
            }
        }
    }

}
