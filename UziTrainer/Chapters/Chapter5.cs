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
    class Chapter5 : Chapter
    {
        public Chapter5(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map5_6()
        {
            var nodeSize = new Size(20, 20);

            screen.Click(new Rectangle(868, 643, 42, 34), EchelonFormationButton);
            screen.Click(new Rectangle(10, 340, 73, 26));
            screen.Click(DeployEchelonButton);

            screen.mouse.DragUpToDown(83, 297, 628);

            var heliport = new Rectangle(894, 133, 35, 35);
            screen.Click(heliport, EchelonFormationButton);
            screen.Click(DeployEchelonButton);
            screen.Click(StartOperationButton);
            Thread.Sleep(4000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(205, 161, 34, 40));
            WaitExecution();
            screen.Click(EndRoundButton);
        }
    }
}
