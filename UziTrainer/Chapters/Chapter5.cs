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
            var nodeSize = new Size(24, 23);

            screen.Click(new Rectangle(new Point(1024, 550), nodeSize), EchelonFormationButton);
            screen.Click(new Rectangle(10, 308, 94, 51));
            screen.Click(DeployEchelonButton);
            screen.mouse.DragUpToDown(773, 107, 715);
            Combat.SanityCheck.Name = $"{root}SanityCheck2";

            var heliport = new Rectangle(new Point(1052, 193), nodeSize);
            screen.Click(heliport, EchelonFormationButton);
            screen.Click(DeployEchelonButton);
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(new Point(843, 181), nodeSize));
            screen.Click(new Rectangle(new Point(673, 234), nodeSize));
            screen.Click(new Rectangle(new Point(464, 201), nodeSize));
            screen.Click(new Rectangle(new Point(235, 233), nodeSize));
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
