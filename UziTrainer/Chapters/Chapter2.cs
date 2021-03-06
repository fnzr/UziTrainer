﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Chapters
{
    class Chapter2 : Chapter
    {
        public Chapter2(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map2_6()
        {
            var commandPost = new Rectangle(220, 395, 41, 40);
            screen.Click(commandPost, EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(3000);

            screen.Click(commandPost);
            screen.Click(commandPost, ResupplyButton);
            screen.Click(ResupplyButton);
            Thread.Sleep(500);
            
            screen.Click(PlanningOffButton);
            screen.mouse.DragUpToDown(522, 192, 530);
            screen.Click(new Rectangle(604, 265, 38, 36));
            WaitExecution();
            screen.Click(EndRoundButton);
        }
    }
}
