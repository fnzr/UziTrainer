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
using System.Threading.Tasks;
using System.Windows.Forms;
using UziTrainer.Scenes;

namespace UziTrainer
{
    static class Program
    {
        public static ManualResetEvent TrainerThread = new ManualResetEvent(false);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(Constants.AppDir);
#if DEBUG
            Directory.CreateDirectory(Constants.DebugDir);
#endif
            Window.Init();
            Scene.Wait(new Query("HomePage/LV", new Rectangle(0,0, 100, 100), true));
            //var Formation = new Formation();
            //Formation.ReplaceDoll(Doll.Get("SVD"), Doll.Get("WA2000"));
            Application.EnableVisualStyles();
            Application.Run(new Form1());
            var sw = Stopwatch.StartNew();            
            
            Trace.WriteLine(sw.Elapsed);
        }
    }

}
