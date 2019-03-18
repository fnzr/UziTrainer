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
            Thread.Sleep(2000);

            screen.Click(commandPost);
            screen.Click(commandPost, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(399, 273, 24, 22));
            screen.Click(new Rectangle(303, 417, 23, 24));
            WaitExecution();
            WaitTurn("2");

            screen.mouse.DragDownToUp(209, 687, 300);
            screen.Click(new Rectangle(304, 270, 22, 23));
            screen.Click(new Rectangle(436, 414, 24, 29));
            screen.Click(new Rectangle(364, 516, 27, 36));
            WaitExecution();
            WaitTurn("3");

            screen.Click(new Rectangle(358, 514, 37, 34));
            screen.Click(new Rectangle(528, 604, 21, 25));
            screen.Click(new Rectangle(802, 578, 29, 26));
            screen.Click(new Rectangle(900, 370, 30, 33));
            WaitExecution();
            WaitTurn("4");

            screen.mouse.DragDownToUp(209, 687, 300);
            screen.Click(new Rectangle(896, 365, 38, 38));
            screen.Click(new Rectangle(762, 354, 23, 28));
            screen.Click(new Rectangle(662, 451, 36, 43));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
