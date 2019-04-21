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
            Thread.Sleep(1000);
            screen.mouse.DragUpToDown(920, 150, 640);
            var echelonPosition = new Rectangle(532, 713, 55, 55);
            screen.Click(echelonPosition);
            UseFairy(echelonPosition);
            screen.Click(PlanningOffButton);            
            screen.Click(new Rectangle(427, 211, 38, 32));
            screen.Click(new Rectangle(816, 230, 50, 50));
            WaitExecution();
            screen.Click(EndRoundButton);
        }
    }
}
