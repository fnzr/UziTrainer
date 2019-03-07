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

            screen.Click(new Rectangle(new Point(1032, 487), nodeSize), EchelonFormationButton);
            screen.Click(new Rectangle(10, 308, 94, 51));
            screen.Click(DeployEchelonButton);
            screen.mouse.DragUpToDown(773, 107, 715);
            Combat.SanityCheck.Name = $"{root}SanityCheck2";

            var heliport = new Rectangle(new Point(1081, 172), nodeSize);
            screen.Click(heliport, EchelonFormationButton);
            screen.Click(DeployEchelonButton);
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(new Point(848, 179), nodeSize));
            screen.Click(new Rectangle(new Point(676, 182), nodeSize));
            screen.Click(new Rectangle(new Point(451, 187), nodeSize));
            screen.Click(new Rectangle(new Point(193, 185), nodeSize));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
        
    }
}
