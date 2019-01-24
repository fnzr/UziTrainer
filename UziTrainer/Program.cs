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
        static int GetLParam(int x, int y)
        {
            return (x & 0xFFFF) | (y & 0xFFFF) << 16;
        }
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
            var swc = Properties.Settings.Default.ScreenWindowClass;
            var swt = Properties.Settings.Default.ScreenWindowTitle;
            var mwc = Properties.Settings.Default.MessageWindowClass;
            var mwt = Properties.Settings.Default.MessageWindowTitle;
            var window = new Window(swc, swt, mwc, mwt);
            var r = window.ImageExists(@"Z:\temp\Screenshot_25.png");
            Console.WriteLine(r);
            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
