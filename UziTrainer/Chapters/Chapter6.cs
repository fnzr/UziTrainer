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
    class Chapter6 : Chapter
    {
        public Chapter6(Screen screen, string mission) : base(screen, mission)
        {
        }

        public void Map6_3N()
        {
            var nodeSize = new Size(35, 38);

            var heliport = new Rectangle(new Point(374, 258), nodeSize);
            var heliport2 = new Rectangle(new Point(750, 365), nodeSize);

            var formation = new Formation(screen);
            var equipEnhancementPopup = new Sample("CombatPage/EquipEnhancementPopup", new Rectangle(722, 506, 157, 43));

            for (int i = 0; ; i++)
            {
                if (i % 2 == 0)
                {
                    screen.Click(heliport, EchelonFormationButton);
                    screen.Click(EchelonFormationButton);
                    formation.ReplaceZas();
                    screen.Click(Formation.ReturnToBase);
                    Thread.Sleep(5000);
                }
                screen.Click(heliport, EchelonFormationButton);
                screen.Click(DeployEchelonButton);
                screen.Click(StartOperationButton);
                if (screen.Exists(equipEnhancementPopup))
                {
                    Program.Pause();                    
                }
                Thread.Sleep(1000);
                UseFairy(heliport);
                screen.Click(PlanningOffButton);
                screen.Click(heliport);
                screen.Click(new Rectangle(new Point(641, 293), nodeSize));
                screen.Click(new Rectangle(new Point(846, 284), nodeSize));
                WaitExecution();
                WaitTurn("2");

                screen.Click(PlanningOnButton);
                screen.Click(heliport2, EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                if (i % 2 == 0)
                {
                    Thread.Sleep(1000);
                    screen.Click(new Rectangle(new Point(846, 284), nodeSize));
                    screen.Click(heliport2);
                    Thread.Sleep(500);
                    screen.Click(new Rectangle(619, 376, 115, 33));
                    Thread.Sleep(3000);
                    screen.Click(heliport2);
                    Thread.Sleep(1500);
                    screen.Click(new Rectangle(931, 659, 126, 38));
                    Thread.Sleep(500);
                    screen.Click(new Rectangle(678, 504, 174, 45));
                    Thread.Sleep(1500);
                }
                else
                {
                    screen.Click(heliport2);
                    screen.Click(heliport2, ResupplyButton);
                    screen.Click(ResupplyButton);
                }
                screen.Click(heliport2);
                Thread.Sleep(1000);
                screen.Click(new Rectangle(931, 659, 126, 38));
                Thread.Sleep(500);
                screen.Click(new Rectangle(678, 504, 174, 45));
                Thread.Sleep(1500);
                screen.Click(TerminateButton);
                screen.Click(new Rectangle(422, 502, 122, 38));
                Program.IncreaseCounter();
            }
            
        }
    }
}
