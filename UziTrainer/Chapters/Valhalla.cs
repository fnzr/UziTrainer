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
    class Valhalla : Chapter
    {
        public Valhalla(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map1_5V()
        {
            var nodeSize = new Size(25, 28);

            var heliport = new Rectangle(new Point(237, 175), nodeSize);
            var commandPost = new Rectangle(new Point(525, 315), nodeSize);

            var dollEnhancementPopup = new Sample("CombatPage/EquipEnhancementPopup", new Rectangle(610, 501, 136, 43));

            for (int i = 0; ; i++)
            {
                screen.Wait(Scenes.Combat.Turn0);
                screen.Click(commandPost, EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                screen.Click(heliport, EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                screen.Click(StartOperationButton);

                if (screen.Exists(dollEnhancementPopup))
                {
                    Program.Pause();
                }

                if (i % 5 == 0)
                {
                    screen.Click(heliport, EchelonFormationButton);
                    screen.Click(ResupplyButton);
                }

                Thread.Sleep(1000);
                screen.Click(PlanningOffButton);
                screen.Click(heliport);
                
                screen.Click(heliport);
                screen.Click(new Rectangle(new Point(240, 466), nodeSize));
                screen.Click(heliport);
                WaitExecution();

                screen.Click(heliport, EchelonFormationButton);
                screen.Click(new Rectangle(new Point(794, 635), new Size(107, 38))); //Retreat Button
                Thread.Sleep(1000);
                screen.Click(new Rectangle(new Point(574, 506), new Size(112, 38))); //Confirm Retreat
                screen.Click(TerminateButton);
                screen.Click(RestartButton);
            }
        }
    }
}
