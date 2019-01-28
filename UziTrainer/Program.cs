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
        private readonly static TraceSource Tracer = new TraceSource("fnzr.UziTrainer");
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
            Tracer.Listeners.Add(new ConsoleTraceListener());
            //var r = window.ImageExists(@"C:\Users\master\Pictures\Screenshot_5.png");
            Window.init();
            var Formation = new Formation();
            //var x = (1, 2, 3, 4);
            var doll = Doll.Get("SVD");
            Formation.SelectDoll(doll);
            //var x = Scene.Exists(new Query(@"Z:\projects\UCT\pics\HomePage\LV.png"));            
            //Console.WriteLine(x);
            //Mouse.Click(1160, 476);
            //Console.WriteLine(r);
            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }

}
