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
    class Chapter
    {
        
        public static readonly Button ExecutePlanButton = new Button("Combat/ExecutePlan", new Rectangle(945, 735, 113, 52), Sample.Negative);
        public static readonly Button DeployEchelonButton = new Button("Combat/DeployOK", new Rectangle(937, 632, 121, 39), Sample.Negative);
        public static readonly Button PlanningOnButton = new Button("Combat/PlanningOn", new Rectangle(4, 692, 109, 30), null);
        public static readonly Button PlanningOffButton = new Button("Combat/PlanningOff", new Rectangle(4, 692, 109, 30), PlanningOnButton);
        public static readonly Button StartOperationButton = new Button("Combat/StartOperation", new Rectangle(858, 729, 212, 58), Sample.Negative);
        public static readonly Button EndRoundButton = new Button("Combat/EndTurn", new Rectangle(937, 738, 119, 55), Sample.Negative, .8f);

        public static readonly Button RestartButton = new Button("Combat/Restart", new Rectangle(357, 501, 100, 37), Sample.Negative);
        public static readonly Button TerminateButton = new Button("Combat/Terminate", new Rectangle(230, 14, 70, 53), null);
        public static readonly Button EchelonFormationButton = new Button("Combat/EchelonFormation", new Rectangle(134, 613, 94, 30), Sample.Negative);
        public static readonly Button ResupplyButton = new Button("Combat/Resupply", new Rectangle(925, 557, 132, 45), Sample.Negative);

        public static readonly Sample MissionSuccessSample = new Sample("Combat/MissionSuccess", new Rectangle(967, 37, 74, 56));
        public static readonly Sample MissionFailedSample = new Sample("Combat/MissionFailed", new Rectangle(967, 37, 74, 56));
        public static readonly Sample CombatPauseSample = new Sample("Combat/Pause", new Rectangle(504, 1, 72, 30));
        public static readonly Sample TurnSample = new Sample("", new Rectangle(343, 25, 66, 58));

        public static readonly Sample FairyCancelSample = new Sample("Combat/FairyCancel", new Rectangle(998, 338, 63, 15));
        public static readonly Button FairyActivateButton = new Button("Combat/FairyActivate", new Rectangle(998, 338, 63, 15), FairyCancelSample);

        static Chapter()
        {
            PlanningOffButton.Next = PlanningOnButton;
        }

        protected Screen screen;
        protected string root;

        public Chapter(Screen screen, string mission)
        {
            this.screen = screen;
            root = $"Missions/{mission}";
            Combat.SanityCheck.Name = $"{root}Sanity";            
            while(!screen.Exists(Combat.SanityCheck, 1000))
            {
                Program.FlashTaskbar();
                Program.Pause();
            }            
            PlanningOnButton.Next = PlanningOffButton;
        }

        protected void WaitBattle()
        {
            screen.Wait(CombatPauseSample);
            while (true)
            {
                if (!screen.Exists(CombatPauseSample))
                {
                    break;
                }
                Thread.Sleep(500);
            }            
            while (true)
            {
                screen.Click(new Rectangle(790, 380, 50, 50));
                if (screen.Exists(Combat.LoadScreenSample, 300))
                {
                    break;
                }                
            }
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
            /*
            Thread.Sleep(1000);
            while (true)
            {
                if (screen.Exists(MissionSuccessSample, 300))
                {
                    return;
                }
                if (screen.Exists(MissionFailedSample, 300))
                {
                    return;
                }
                if (screen.Exists(PlanningOffButton, 300))
                {
                    break;
                }
                if (screen.Exists(EndTurnButton, 300))
                {
                    //Program.WriteLog("Executing Plan");
                }
                if (screen.Exists(CombatPauseSample, 300))
                {
                    WaitBattle();
                }
            }
            Thread.Sleep(500);
            */
        }

        public void WaitTurn(string turn)
        {
            screen.Click(EndRoundButton);
            TurnSample.Name = "Combat/Turn" + turn;
            var samples = new Sample[] { CombatPauseSample, TurnSample, MissionSuccessSample, MissionFailedSample, TerminateButton };
            while (true)
            {
                var found = screen.ExistsAny(samples);
                if (found == 0)
                {
                    WaitBattle();
                }
                if (found == 2 || found == 3)
                {
                    return;
                }
                if (found == 1)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            // G&K turn started
            PlanningOffButton.Next = null;
            while (true)
            {   
                if (screen.Exists(PlanningOffButton, 1000))
                {
                    screen.Click(PlanningOffButton);                    
                    if (screen.Exists(PlanningOnButton))
                    {
                        break;
                    }
                }
                if (screen.Exists(MissionFailedSample, 1000))
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            PlanningOffButton.Next = PlanningOnButton;
        }

        public void UseFairy(Rectangle echelonPosition)
        {
            if ((Properties.Settings.Default.FairyInterval != -1) && (Program.RunCounter % Properties.Settings.Default.FairyInterval == 0))
            {
                bool planningOn = false;
                if (screen.Exists(PlanningOnButton))
                {
                    planningOn = true;
                    screen.Click(PlanningOnButton);
                }                
                screen.Click(echelonPosition, FairyActivateButton);
                screen.Click(FairyActivateButton);
                screen.Click(echelonPosition);
                Thread.Sleep(1000);
                if (planningOn)
                {
                    screen.Click(PlanningOffButton);
                }
            }
        }
    }
}
