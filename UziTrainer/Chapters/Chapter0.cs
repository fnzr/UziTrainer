using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UziTrainer.Scenes;
using UziTrainer.Window;

namespace UziTrainer.Chapters
{
    class Chapter0 : Chapter
    {

        Random random = new Random();
        public Chapter0(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map0_2()
        {
            var commandPost = new Rectangle(530, 388, 46, 43);
            screen.Click(commandPost, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            var heliport = new Rectangle(369, 385, 35, 39);
            screen.Click(heliport);
            screen.Click(DeployEchelonButton);
            
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);
            
            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);
            Thread.Sleep(1000);
            
            screen.Click(commandPost);
            screen.Click(PlanningOffButton);            
            screen.Click(new Rectangle(484, 143, 23, 21));
            screen.Click(new Rectangle(674, 150, 39, 34));
            WaitExecution();

            var sleep = random.Next(60000, 180000);
            System.Diagnostics.Trace.WriteLine($"Sleeping for {sleep / 1000} seconds");
            Thread.Sleep(sleep);

            screen.Click(EndRoundButton);
        }
    }
}
