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
    class Chapter1 : Chapter
    {        
        public Chapter1(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map1_6()
        {
            var commandPost = new Rectangle(214, 269, 38, 40);
            screen.Click(commandPost, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(2500);

            screen.Click(commandPost);
            screen.Click(commandPost, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(360, 658, 41, 41));
            screen.Click(new Rectangle(658, 597, 41, 45));
            WaitExecution();
            screen.Click(EndRoundButton);
        }
    }
}
