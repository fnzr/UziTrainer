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

        private readonly string root;
        public Chapter0(Screen screen, string mission) : base(screen, mission)
        {
            root = $"Missions/{mission}/";
        }

        public void Map0_2()
        {
            Size nodeSize = new Size(50, 35);

            screen.Click(new Button("", new Rectangle(new Point(589, 334), nodeSize), EchelonFormationButton));
            screen.Click(DeployEchelonButton);

            screen.Click(new Button("", new Rectangle(new Point(201, 336), nodeSize), EchelonFormationButton));
            screen.Click(DeployEchelonButton);
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(PlanningOffButton);
            Button CommandPost = new Button("", new Rectangle(new Point(589, 334), nodeSize), DeployEchelonButton);
            screen.Click(CommandPost);
            
            screen.Click(new Rectangle(471, 274, 54, 45));
            screen.mouse.DragUpToDown(700, 104, 734);

            var plan2 = new Sample(root + "Plan2", new Rectangle(479, 607, 45, 37));
            screen.Click(new Button("", new Rectangle(new Point(495, 562), nodeSize), plan2));
            
            screen.Click(new Rectangle(new Point(645, 369), nodeSize));
            screen.Click(new Rectangle(new Point(494, 275), nodeSize));

            WaitExecution();
        }
    }
}
