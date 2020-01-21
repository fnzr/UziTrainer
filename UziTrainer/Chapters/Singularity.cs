using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Chapters
{
    class Singularity : Chapter
    {
        Random random = new Random();
        public Singularity(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void PromotionIV()
        {
            var heliport = new Rectangle(646, 535, 20, 20);
            var retreatNode = new Rectangle(590, 513, 22, 17);
            for (int i = 0; ; i++)
            {                
                screen.Click(heliport, EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                screen.Click(new Rectangle(277, 488, 20, 20), EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                screen.Click(StartOperationButton);
                Thread.Sleep(2000);

                if (i % 2 == 0)
                {
                    screen.Click(heliport);
                    screen.Click(heliport, ResupplyButton);
                    screen.Click(ResupplyButton);
                    Thread.Sleep(2000);
                }
                else
                {
                    screen.Click(heliport);
                }                
                screen.Click(PlanningOffButton);
                screen.Click(new Rectangle(618, 423, 16, 20));
                screen.Click(new Rectangle(636, 473, 20, 20));
                screen.Click(new Rectangle(576, 435, 14, 17));
                screen.Click(retreatNode);
                WaitExecution();

                var sleep = random.Next(60000, 180000);
                System.Diagnostics.Trace.WriteLine($"Sleeping for {sleep / 1000} seconds");
                Thread.Sleep(sleep);

                screen.Click(retreatNode);
                screen.Click(retreatNode, ResupplyButton);
                screen.Click(new Rectangle(792, 632, 111, 40)); //Retreat
                screen.Click(new Rectangle(573, 501, 120, 43), TerminateButton); //Confirm Retreat

                screen.Click(TerminateButton);
                screen.Click(RestartButton);
                Program.IncreaseCounter();
            }
        }
    }
}
