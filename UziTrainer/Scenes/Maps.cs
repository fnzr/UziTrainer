using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Maps
    {
        public static readonly Button ExecutePlanButton = new Button("Combat/ExecutePlan", new Rectangle(945, 735, 113, 52), Sample.Negative);
        public static readonly Button DeployEchelonButton = new Button("Combat/DeployOK", new Rectangle(928, 623, 111, 40), Sample.Negative);
        public static readonly Button PlanningOnButton = new Button("Combat/PlanningOn", new Rectangle(4, 692, 109, 30), null);
        public static readonly Button PlanningOffButton = new Button("Combat/PlanningOff", new Rectangle(4, 692, 109, 30), PlanningOnButton);
        public static readonly Button StartOperationButton = new Button("Combat/StartOperation", new Rectangle(858, 729, 212, 58), Sample.Negative);
        public static readonly Button EndRoundButton = new Button("Combat/EndTurn", new Rectangle(937, 738, 119, 55), Sample.Negative, .8f);

        public static readonly Button RestartButton = new Button("Combat/Restart", new Rectangle(357, 501, 100, 37), Sample.Negative);
        public static readonly Button TerminateButton = new Button("Combat/Terminate", new Rectangle(230, 14, 70, 53), RestartButton);
        public static readonly Button EchelonFormationButton = new Button("Combat/EchelonFormation", new Rectangle(145, 621, 52, 31), Sample.Negative);
        public static readonly Button ResupplyButton = new Button("Combat/Resupply", new Rectangle(925, 557, 132, 45), Sample.Negative);
        private Combat Combat;
        private Formation Formation;
        private Screen screen;

        private Random random = new Random();

        public Maps(Screen screen)
        {
            this.Combat = new Combat(screen);
            this.Formation = new Formation(screen);
            this.screen = screen;
        }
        protected void WaitExecution()
        {
            screen.Click(ExecutePlanButton);
            while (true)
            {
                /* Maybe executed all planning */
                if (screen.Exists(PlanningOffButton, 0))
                {
                    Thread.Sleep(5000);
                    /* If PlanningOff dissapared, we're not done yet */
                    if (screen.Exists(PlanningOffButton, 0))
                    {
                        break;
                    }
                }
                Thread.Sleep(5000);
            }
        }

        public void Drag0_2()
        {
            this.Combat.StartMission("0_2");

            var commandPost = new Rectangle(530, 388, 46, 43);
            screen.Click(commandPost, EchelonFormationButton);

            if (Properties.Settings.Default.IsCorpseDragging)
            {
                screen.Click(EchelonFormationButton);
                Formation.ReplaceCorpseDragger();
                screen.Click(Formation.ReturnToBase);
                screen.Wait(Combat.Turn0);
                screen.Click(commandPost, EchelonFormationButton);
            }
            screen.Click(DeployEchelonButton);

            var heliport = new Rectangle(369, 385, 35, 39);
            screen.Click(heliport);
            screen.Click(DeployEchelonButton);

            screen.Click(StartOperationButton);
            Thread.Sleep(2000);

            screen.Click(heliport);
            screen.Click(heliport, ResupplyButton);
            screen.Click(ResupplyButton);
            Thread.Sleep(1000);

            screen.Click(commandPost);
            screen.Click(PlanningOffButton);
            screen.Click(new Rectangle(484, 143, 23, 21));
            screen.Click(new Rectangle(674, 150, 39, 34));
            WaitExecution();

            var sleep = random.Next(60000, 180000);
            System.Diagnostics.Trace.WriteLine($"Sleeping for {sleep / 1000} seconds");
            Thread.Sleep(sleep);

            screen.Click(EndRoundButton);
            Thread.Sleep(7000);
            screen.Click(new Rectangle(208, 6, 427, 36), Combat.CombatScene, 700);
        }
    }
}
