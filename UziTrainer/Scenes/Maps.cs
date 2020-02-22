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
        public static readonly Button SwitchButton = new Button("Combat/switch", Screen.FullArea, Sample.Negative, .8f, SwitchClickArea);

        public static readonly Sample CriticalSample = new Sample("Combat/Critical", Screen.FullArea, null, 0.83f);
        private Combat Combat;
        private Formation Formation;
        private Screen screen;

        private Random random = new Random();

        private static Rectangle SwitchClickArea(Point arg)
        {            
            return new Rectangle(arg.X - 5, arg.Y - 2, 10, 5);
        }

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
            var requiresRepair = false;
            while (true)
            {
                this.Combat.StartMission("0_2");

                var commandPost = new Rectangle(530, 388, 46, 43);
                screen.Click(commandPost, EchelonFormationButton);

                if (Properties.Settings.Default.IsCorpseDragging)
                {
                    screen.Click(EchelonFormationButton);
                    Formation.ReplaceCorpseDragger();
                    screen.Click(Formation.ReturnToBase, Combat.Turn0);
                    screen.Click(commandPost, EchelonFormationButton);
                }
                if (requiresRepair)
                {
                    Program.Pause();
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

                var sleep = random.Next(5000, 20000);
                System.Diagnostics.Trace.WriteLine($"Sleeping for {sleep / 1000} seconds");
                Thread.Sleep(sleep);

                requiresRepair = screen.Exists(CriticalSample, 1000);

                screen.Click(EndRoundButton);
                Thread.Sleep(7000);
                screen.Click(new Rectangle(19, 407, 116, 378), Combat.CombatScene, 1500);
            }            
        }

        public static readonly Sample CT_Home = new Sample("ct/home", new Rectangle(43, 206, 93, 124), null, 0.83f);
        public static readonly Sample CT_ReturnBase = new Sample("ct/return_base", new Rectangle(78, 152, 87, 39), null, 0.83f);
        public static readonly Button CT_ConfirmStart = new Button("ct/confirm_start", new Rectangle(947, 712, 77, 46), Combat.Turn0);

        public void CT_Scarecrow()
        {
            for(int i = 0; i<9; i++)
            {
                var cp = new Rectangle(570, 420, 27, 30);
                screen.Wait(CT_Home);
                screen.Click(new Rectangle(609, 309, 71, 44), CT_ConfirmStart);
                screen.Click(CT_ConfirmStart);
                screen.Wait(Combat.Turn0);
                screen.Click(cp, DeployEchelonButton);
                screen.Click(DeployEchelonButton);
                screen.Click(StartOperationButton);

                screen.Click(cp);
                screen.Click(cp, ResupplyButton);
                screen.Click(ResupplyButton);
                screen.Click(PlanningOffButton);
                screen.Click(new Rectangle(594, 138, 20, 21));
                screen.Click(ExecutePlanButton);

                Thread.Sleep(TimeSpan.FromSeconds(60));
                screen.Wait(CT_ReturnBase);
                screen.Click(new Rectangle(19, 407, 116, 378), CT_Home, 3000);
            }
        }

        public void CT_PythonFarm()
        {
            while (true)
            {
                screen.Wait(Combat.Turn0);
                var cp = new Rectangle(580, 482, 22, 22);
                screen.Click(cp, DeployEchelonButton);
                screen.Click(DeployEchelonButton);
                screen.Click(StartOperationButton);
                screen.Click(cp);

                screen.Click(PlanningOffButton);
                screen.Click(new Rectangle(431, 415, 11, 12));
                screen.Click(new Rectangle(501, 285, 13, 12));
                WaitExecution();

                screen.Click(TerminateButton);
                screen.Click(RestartButton);
                /*
                screen.Wait(CT_Home);
                screen.Click(new Rectangle(614, 338,63, 31), CT_ConfirmStart);
                screen.Click(CT_ConfirmStart);
                screen.Wait(Combat.Turn0);

                var cp = new Rectangle(580, 482, 22, 22);
                screen.Click(cp, DeployEchelonButton);
                screen.Click(DeployEchelonButton);
                screen.Click(StartOperationButton);
                screen.Click(cp);
                screen.Click(cp, ResupplyButton);
                screen.Click(ResupplyButton);
                screen.Click(PlanningOffButton);

                screen.Click(new Rectangle(644, 286, 14, 14));
                WaitExecution();

                var heliport = new Rectangle(640, 474, 15, 16);
                screen.Click(heliport);
                screen.Click(DeployEchelonButton);

                screen.Click(heliport);
                screen.Click(heliport, ResupplyButton);
                screen.Click(ResupplyButton);

                var node = new Rectangle(641, 402, 15, 11);
                screen.Click(node);
                screen.Click(SwitchButton);
                screen.Click(new Rectangle(297, 226, 43, 121));

                screen.Click(heliport);
                screen.Click(heliport, ResupplyButton);
                screen.Click(ResupplyButton);

                screen.Click(PlanningOffButton);

                screen.Click(new Rectangle(297, 226, 43, 121));
                screen.Click(node);
                screen.Click(new Rectangle(500, 401, 16, 11));

                screen.Click(new Rectangle(297, 226, 43, 121));

                screen.Click(heliport);
                screen.Click(new Rectangle(503, 530, 11, 14));

                WaitExecution();

                Thread.Sleep(TimeSpan.FromSeconds(60));
                screen.Wait(CT_ReturnBase);
                screen.Click(new Rectangle(19, 407, 116, 378), CT_Home, 3000);
                */
            }            
        }
    }
}
