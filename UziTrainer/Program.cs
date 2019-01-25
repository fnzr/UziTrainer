using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UziTrainer
{
    static class Program
    {        
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

            //var r = window.ImageExists(@"C:\Users\master\Pictures\Screenshot_5.png");
            Window.init();
            Scene.Click(1160, 476);
            //Console.WriteLine(r);
            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
