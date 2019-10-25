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
            screen.Click(EndRoundButton);
        }
    }
}
