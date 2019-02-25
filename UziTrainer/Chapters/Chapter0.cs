﻿using System;
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

            var heliport = new Button("", new Rectangle(new Point(201, 336), nodeSize), EchelonFormationButton);
            screen.Click(heliport, true);
            screen.Click(DeployEchelonButton);
            if (true)
            {
                return;
            }
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            heliport.Next = ResupplyButton;
            screen.Click(heliport);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            Button CommandPost = new Button("", new Rectangle(new Point(589, 334), nodeSize), null);
            screen.Click(CommandPost);
            
            screen.Click(new Rectangle(new Point(471, 274), nodeSize));
            screen.mouse.DragUpToDown(700, 104, 734);

            var plan2 = new Sample(root + "Plan2", new Rectangle(479, 607, 45, 37));
            screen.Click(new Button("", new Rectangle(new Point(495, 562), nodeSize), plan2));
            
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
