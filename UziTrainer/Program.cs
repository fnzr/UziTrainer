using static UziTrainer.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
            //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Trace.AutoFlush = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Tap(Samples.HomeCombat);
            //Formation.ReplaceDragger(Samples.HK416,Samples.SOPMOD);
            Exists(Samples.CombatMissionClicked);
            /*
            while (true)
            {
                Maps.N0_2();
            }
            */
            //Maps.PerformRepairs();
            //Exists(Samples.LowHP, 0);
            Console.WriteLine("Done");
        }
    }
}
