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
    class Chapter0 : Chapter
    {
        public Chapter0(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map0_2()
        {
            Size nodeSize = new Size(50, 35);

            screen.Click(new Rectangle(new Point(621, 360), nodeSize), EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            var heliport = new Rectangle(new Point(225, 350), nodeSize);
            screen.Click(heliport);
            screen.Click(DeployEchelonButton);
            
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);
            
            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(new Point(621, 360), nodeSize));
            
            screen.Click(new Rectangle(new Point(471, 278), nodeSize));
            screen.mouse.DragUpToDown(700, 104, 734);

            var plan2 = new Sample(root + "Plan2", new Rectangle(479, 607, 45, 37), null, .90f);
            screen.Click(new Rectangle(new Point(495, 562), nodeSize), plan2);
            
            screen.Click(new Rectangle(new Point(645, 369), nodeSize));
            screen.Click(new Rectangle(new Point(494, 275), nodeSize));

            WaitExecution();
            WaitTurn("2");

            screen.Click(new Rectangle(new Point(501, 279), nodeSize));
            screen.Click(new Rectangle(new Point(786, 275), nodeSize));
            screen.Click(new Rectangle(new Point(964, 299), nodeSize));            
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
