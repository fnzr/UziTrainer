using System.Drawing;
using System.Threading;

namespace UziTrainer.Chapters
{
    class Chapter
    {
        protected static readonly Point StartOperationN = new Point(1126, 684);
        protected static readonly Query DeployEchelon = new Query("Combat/DeployOK", new Rectangle(1135, 661, 25, 25));
        protected static readonly Point PlanningMode = new Point(76, 622);
        protected static readonly Point ExecutePlan = new Point(1187, 684);
        protected static readonly Point EndRound = new Point(1176, 691);
        protected static readonly Query Resupply = new Query("Combat/Resupply", new Rectangle(1166, 583, 30, 30));
        protected static readonly Query Retreat = new Query("Combat/Retreat", new Rectangle(917, 646, 170, 70));
        protected static readonly Query CombatPause = new Query("Combat/Retreat", new Rectangle(917, 646, 170, 70));

        public Chapter()
        {
        }


        protected void WaitBattle()
        {
            Screen.Wait(CombatPause);
            Program.WriteLog("In Battle");
            
            while (true)
            {
                if (!Screen.Exists(CombatPause))
                {
                    break;
                }
                Thread.Sleep(500);
            }
            var LoadScreen = new Query("LoadScreen");
            while (true)
            {
                if (Screen.Exists(LoadScreen, 300))
                {
                    break;
                }
                Mouse.Click(630, 365, 100, 100);
            }
            Program.WriteLog("Finished Battle");
        }

        protected void WaitExecution()
        {
            Program.WriteLog("Executing Plan");
            Mouse.Click(ExecutePlan);
            Thread.Sleep(1000);
            while (true)
            {
                if (Screen.Exists(new Query("Combat/MissionSuccess", new Rectangle(1196, 121, 50, 50), 1000)))
                {
                    return;
                }
                if (Screen.Exists(new Query("Combat/MissionFailed", new Rectangle(1192, 125, 50, 50), 1000)))
                {
                    return;
                }
                if (Screen.Exists(new Query("Combat/Planning", new Rectangle(1, 567, 173, 100))))
                {
                    break;
                }
                if (Screen.Exists(new Query("Combat/EndTurn", new Rectangle(1097, 646, 150, 90))))
                {
                    Program.WriteLog("Executing Plan");
                }
                else if (Screen.Exists(CombatPause)){
                    WaitBattle();
                }
            }
            Thread.Sleep(500);
        }

        protected void WaitTurn(string turn)
        {
            Mouse.Click(EndRound);
            Program.WriteLog("Waiting Turn " + turn);
            while (true)
            {
                if (Screen.Exists(new Query("Combat/Turn" + turn)))
                {
                    break;
                }
                if (Screen.Exists(new Query("Combat/MissionSuccess", new Rectangle(1196, 121, 50, 50)), 1000))
                {
                    return;
                }
                if (Screen.Exists(new Query("Combat/MissionFailed", new Rectangle(1192, 125, 50, 50)), 1000))
                {
                    return;
                }
                if (Screen.Exists(new Query("Combat/Terminate", new Rectangle(263, 45, 100, 70)), 1000))
                {
                    Program.WriteLog("SF moving");
                }
                if (Screen.Exists(CombatPause, 1000))
                {
                    WaitBattle();
                }
            }
            Program.WriteLog("G&K turn started");
            while (true)
            {
                if (Screen.Exists(new Query("Combat/Planning"), 1000, out Point coordinates))
                {
                    Mouse.Click(coordinates);
                    if (Screen.Exists(new Query("Combat/PlanningReady")))
                    {
                        break;
                    }
                }
                if (Screen.Exists(new Query("Combat/MissionFailed", new Rectangle(1192, 125, 50, 50)), 1000))
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
