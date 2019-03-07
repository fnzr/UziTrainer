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
        
        public static readonly Rectangle ExecutePlanButton = new Rectangle(1112, 657, 146, 64);
        public static readonly Button DeployEchelonButton = new Button("Combat/DeployOK", new Rectangle(1106, 653, 148, 53), Combat.SanityCheck);
        public static readonly Button PlanningOnButton = new Button("Combat/PlanningOn", new Rectangle(6, 607, 122, 30), Combat.SanityCheck);
        public static readonly Button PlanningOffButton = new Button("Combat/PlanningOff", new Rectangle(6, 607, 122, 30), null);
        public static readonly Rectangle StartOperationButton = new Rectangle(1012, 651, 250, 80);
        public static readonly Button EndTurnButton = new Button("Combat/EndTurn", new Rectangle(1097, 646, 150, 90), Sample.Negative);
        public static readonly Button TerminateButton = new Button("Combat/Terminate", new Rectangle(263, 45, 100, 70), null);
        public static readonly Button EchelonFormationButton = new Button("Combat/EchelonFormation", new Rectangle(159, 636, 162, 36), null);
        public static readonly Button ResupplyButton = new Button("Combat/Resupply", new Rectangle(1089, 569, 180, 45), Combat.SanityCheck);

        public static readonly Sample MissionSuccessSample = new Sample("Combat/MissionSuccess", new Rectangle(1196, 121, 50, 50));
        public static readonly Sample MissionFailedSample = new Sample("Combat/MissionFailed", new Rectangle(1196, 121, 50, 50));
        public static readonly Sample CombatPauseSample = new Sample("Combat/Pause", new Rectangle(917, 646, 170, 70));
        public static readonly Sample TurnSample = new Sample("", Screen.FullArea);

        static Chapter()
        {
            PlanningOffButton.Next = PlanningOnButton;
        }

        protected Screen screen;
        protected string root;

        public Chapter(Screen screen, string mission)
        {
            this.screen = screen;
            root = $"Missions/{mission}/";
            Combat.SanityCheck.Name = $"{root}SanityCheck";
            while(!screen.Exists(Combat.SanityCheck, 1000))
            {
                screen.mouse.ZoomOut();
            }            
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
                if (screen.Exists(Combat.LoadScreenSample, 300))
                {
                    break;
                }
                screen.Click(new Rectangle(790, 380, 50, 50));
            }
        }

        protected void WaitExecution()
        {
            screen.Click(ExecutePlanButton);
            Thread.Sleep(1000);
            while (true)
            {
                if (screen.Exists(MissionSuccessSample, 1000))
                {
                    return;
                }
                if (screen.Exists(MissionFailedSample, 1000))
                {
                    return;
                }
                if (screen.Exists(PlanningOffButton))
                {
                    break;
                }
                if (screen.Exists(EndTurnButton))
                {
                    //Program.WriteLog("Executing Plan");
                }
                else if (screen.Exists(CombatPauseSample))
                {
                    WaitBattle();
                }
            }
            Thread.Sleep(500);
        }

        protected void WaitTurn(string turn)
        {
            screen.Click(EndTurnButton);
            TurnSample.Name = "Combat/Turn" + turn;
            while (true)
            {
                if (screen.Exists(CombatPauseSample, 200))
                {
                    WaitBattle();
                }
                if (screen.Exists(TurnSample))
                {
                    break;
                }
                if (screen.Exists(MissionSuccessSample, 200))
                {
                    return;
                }
                if (screen.Exists(MissionFailedSample, 200))
                {
                    return;
                }
                if (screen.Exists(TerminateButton, 200))
                {
                    //SF moving
                }                
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
    }
}
