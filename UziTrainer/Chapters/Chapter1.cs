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
            Size nodeSize = new Size(35, 26);

            var commandPost = new Rectangle(new Point(251, 349), nodeSize);
            screen.Click(commandPost, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(commandPost);
            screen.Click(commandPost, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(new Point(468, 353), nodeSize));
            screen.Click(new Rectangle(new Point(357, 522), nodeSize));
            WaitExecution();
            WaitTurn("2");

            screen.mouse.DragDownToUp(670, 600, 150);
            var echelonSelected = new Sample($"{root}EchelonSelected", new Rectangle(368, 168, 12, 32));
            screen.Click(new Rectangle(353, 126, 42, 31), echelonSelected);
            screen.Click(new Rectangle(new Point(511, 283), nodeSize));
            screen.Click(new Rectangle(new Point(419, 398), nodeSize));
            WaitExecution();
            WaitTurn("3");

            screen.mouse.DragDownToUp(550, 600, 200);
            screen.Click(new Rectangle(new Point(422, 393), nodeSize));
            screen.Click(new Rectangle(new Point(620, 501), nodeSize));
            screen.Click(new Rectangle(new Point(947, 472), nodeSize));
            screen.Click(new Rectangle(new Point(1055, 224), nodeSize));
            WaitExecution();
            WaitTurn("4");

            screen.mouse.DragDownToUp(550, 600, 200);
            screen.Click(new Rectangle(new Point(1055, 224), nodeSize));
            screen.Click(new Rectangle(new Point(896, 212), nodeSize));
            screen.Click(new Rectangle(new Point(779, 327), nodeSize));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
