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
            screen.Click(new Rectangle(528, 369, 61, 65), EchelonFormationButton);
            screen.Click(DeployEchelonButton);

            var heliport = new Rectangle(194, 373, 47, 47);
            screen.Click(heliport);
            screen.Click(DeployEchelonButton);
            
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);
            
            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(529, 373, 57, 53));            
            screen.Click(new Rectangle(402, 302, 36, 35));
            screen.Click(new Rectangle(423, 112, 54, 53));
            screen.mouse.DragUpToDown(920, 150, 640);
            
            screen.Click(new Rectangle(550, 287, 36, 31));
            screen.Click(new Rectangle(424, 209, 39, 26));

            WaitExecution();
            WaitTurn("2");

            var echelonPosition = new Rectangle(423, 208, 38, 30);
            UseFairy(echelonPosition);

            screen.Click(echelonPosition);
            screen.Click(new Rectangle(669, 203, 39, 31));
            screen.Click(new Rectangle(816, 225, 57, 48));            
            WaitExecution();
            screen.Click(EndTurnButton);
        }
    }
}
