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
            var nodeSize = new Size(27, 25);

            var heliport = new Rectangle(new Point(365, 222), nodeSize);
            var equipEnhancementPopup = new Sample("CombatPage/EquipEnhancementPopup", new Rectangle(610, 501, 136, 43));

            for (int i = 0; ; i++)
            {
                screen.Click(heliport, EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                screen.Click(StartOperationButton);

                if (screen.Exists(equipEnhancementPopup))
                {
                    Program.Pause();
                }

                Thread.Sleep(1000);

                screen.Click(PlanningOffButton);
                screen.Click(heliport);
                screen.Click(new Rectangle(new Point(832, 249), nodeSize));

                screen.Click(ExecutePlanButton);
                Thread.Sleep(1500);
                screen.Click(OutOfAmmoSample);
                screen.Wait(OutOfAmmoSample);
                Thread.Sleep(1500);
                screen.Click(OutOfAmmoSample);

                screen.Wait(TerminateButton);
                Thread.Sleep(1500);
                screen.Click(TerminateButton);
                screen.Click(RestartButton);
                Program.IncreaseCounter();
            }
        }

        /* Zas Dragging. Replaced by Airstrike */
        /*
        public void Map6_3N()
        {
            var nodeSize = new Size(27, 25);

            var heliport = new Rectangle(new Point(365, 222), nodeSize);
            var heliport2 = new Rectangle(new Point(565, 330), nodeSize);

            var formation = new Formation(screen);
            var equipEnhancementPopup = new Sample("CombatPage/EquipEnhancementPopup", new Rectangle(610, 501, 136, 43));

            for (int i = 0; ; i++)
            {                
                if (i % 2 == 0)
                {
                    screen.Click(heliport, EchelonFormationButton);
                    screen.Click(EchelonFormationButton);
                    formation.ReplaceZas();
                    screen.Click(Formation.ReturnToBase, StartOperationButton, 5000);
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
                screen.Click(new Rectangle(new Point(832, 249), nodeSize));
                WaitExecution();
                WaitTurn("2");

                screen.Click(PlanningOnButton);
                screen.Click(heliport2, EchelonFormationButton);
                screen.Click(DeployEchelonButton);

                if (i % 2 == 0)
                {
                    Thread.Sleep(1000);
                    screen.Click(new Rectangle(new Point(657, 252), nodeSize));
                    screen.Click(heliport2);
                    Thread.Sleep(500);
                    screen.Click(new Rectangle(450, 339, 105, 32)); //Switch
                    Thread.Sleep(3000);
                }
                else
                {
                    screen.Click(heliport2);
                    screen.Click(heliport2, ResupplyButton);
                    screen.Click(ResupplyButton);
                }
                screen.Click(heliport2, ResupplyButton);
                Thread.Sleep(1500);
                screen.Click(new Rectangle(783, 630, 124, 42)); // Retreat
                Thread.Sleep(500);
                screen.Click(new Rectangle(573, 502, 107, 38)); // Confirm
                Thread.Sleep(1000);
                screen.Click(TerminateButton);
                screen.Click(RestartButton);
                Program.IncreaseCounter();
            }            
        }
        */

        public void Map6_6()
        {
            var commandPost = new Rectangle(115, 169, 37, 35);
            screen.Click(commandPost, EchelonFormationButton);
            screen.Click(DeployEchelonButton);
            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(commandPost);
            screen.Click(commandPost, ResupplyButton);
            screen.Click(ResupplyButton);
            Thread.Sleep(1000);

            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(569, 179, 33, 32));
            screen.Click(new Rectangle(639, 338, 34, 33));
            WaitExecution();
            screen.Click(EndRoundButton);

        }
    }
}
