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
    class Chapter2 : Chapter
    {
        public Chapter2(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map2_6()
        {
            var nodeSize = new Size(25, 22);

            var commandPost = new Rectangle(new Point(265, 367), nodeSize);
            screen.Click(commandPost, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(3000);

            screen.Click(commandPost);
            screen.Click(commandPost, ResupplyButton);
            screen.Click(ResupplyButton);
            Thread.Sleep(500);

            screen.Click(PlanningOffButton);
            screen.mouse.DragUpToDown(660, 150, 700);            
            screen.Click(new Rectangle(new Point(236, 386), nodeSize));
            screen.Click(new Rectangle(new Point(359, 248), nodeSize));
            WaitExecution();
            WaitTurn("2");

            screen.Click(new Rectangle(new Point(361, 247), nodeSize));
            screen.Click(new Rectangle(new Point(571, 264), nodeSize));
            screen.Click(new Rectangle(new Point(714, 340), nodeSize));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
