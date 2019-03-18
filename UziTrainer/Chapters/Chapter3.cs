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
            var heliport = new Rectangle(591, 279, 34, 35);
            screen.Click(heliport, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(new Rectangle(878, 174, 39, 37), EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(469, 298, 20, 20));
            screen.Click(new Rectangle(337, 439, 22, 23));
            screen.Click(new Rectangle(309, 592, 33, 33));
            screen.Click(new Rectangle(424, 666, 32, 34));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
