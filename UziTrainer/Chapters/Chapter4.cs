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
    class Chapter4 : Chapter
    {
        public Chapter4(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map4_6()
        {
            var nodeSize = new Size(24, 23);

            screen.Click(new Rectangle(876, 585, 35, 35), EchelonFormationButton);
            screen.Click(new Rectangle(9, 337, 77, 27));
            screen.Click(DeployEchelonButton);
            screen.mouse.DragUpToDown(400, 160, 560);

            var heliport = new Rectangle(917, 117, 37, 34);
            screen.Click(heliport, EchelonFormationButton);
            screen.Click(DeployEchelonButton);
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(161, 129, 36, 34));
            WaitExecution();
            screen.Click(EndRoundButton);
        }
        
    }
}
