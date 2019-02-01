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
            //Trace.Listeners.Add(new ConsoleTraceListener());
            //var r = window.ImageExists(@"C:\Users\master\Pictures\Screenshot_5.png");
            Window.Init();
            //Window.CaptureBitmap();
            //var q = new Query("FormationPage");
            //Scene.Click(q.Create("Filter", new Rectangle(1106, 269, 110, 45)));
            var Formation = new Formation();
            //Scene.Exists(Query.Create("Dolls/MG5"));
            //var doll = Doll.Get("SVD");
            //Formation.SelectDoll(doll); 
            Formation.ReplaceDoll(Doll.Get("WA2000"), Doll.Get("SVD"));
            //Scene.Exists(Query.Create("Dolls/SVD"));
            //var sw = Stopwatch.StartNew();
            //Scene.Exists(Query.Create("Dolls/MG5"));
            //Trace.WriteLine(sw.Elapsed);
            Application.EnableVisualStyles();
            Application.Run(new Form1());
            var sw = Stopwatch.StartNew();            
            
            Trace.WriteLine(sw.Elapsed);
        }
    }

}
