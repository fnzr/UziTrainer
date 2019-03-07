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
    class Chapter3 : Chapter
    {
        public Chapter3(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map3_6()
        {
            var nodeSize = new Size(21, 21);

            var heliport = new Rectangle(new Point(699, 362), nodeSize);
            screen.Click(heliport, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(new Rectangle(new Point(1036, 240), nodeSize), EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(new Point(555, 386), nodeSize));
            screen.Click(new Rectangle(new Point(399, 550), nodeSize));
            screen.mouse.DragDownToUp(485, 730, 185);
            screen.Click(new Rectangle(new Point(366, 352), nodeSize));
            screen.Click(new Rectangle(new Point(498, 439), nodeSize));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
