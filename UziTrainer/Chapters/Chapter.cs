﻿using System.Drawing;
using System.Threading;

namespace UziTrainer.Chapters
{
    class Chapter
    {
        protected static readonly Point START_OPERATION = new Point(1126, 684);
        protected static readonly Query DEPLOY_ECHELON = new Query("Combat/DeployOK", new Rectangle(1135, 661, 25, 25));
        protected static readonly Point PLANNING_MODE = new Point(76, 622);
        protected static readonly Point EXECUTE_PLAN = new Point(1187, 684);
        protected static readonly Point END_ROUND = new Point(1176, 691);
        protected static readonly Query RESUPPLY = new Query("Combat/Resupply", new Rectangle(1166, 583, 30, 30));
        protected static readonly Query RETREAT = new Query("Combat/Retreat", new Rectangle(917, 646, 170, 70));

        protected static void WaitBattle()
        {
            Scene.Wait(new Query("Combat/CombatPause", new Rectangle(557, 29, 100, 30)));
            Program.main.WriteLog("In Battle");
            while (true)
            {
                if (!Scene.Exists(new Query("Combat/CombatPause", new Rectangle(557, 29, 100, 30))))
                {
                    break;
                }
                Thread.Sleep(500);
            }
            while (true)
            {
                if (Scene.Exists(new Query("LoadScreen"), 300))
                {
                    break;
                }
                Mouse.Click(630, 365, 100);
            }
            Program.main.WriteLog("Finished Battle");
        }

        protected static void WaitExecution()
        {
            Program.main.WriteLog("Executing Plan");
            Mouse.Click(EXECUTE_PLAN);
            Thread.Sleep(1000);
            while (true)
            {
                if (Scene.Exists(new Query("Combat/MissionSuccess", new Rectangle(1196, 121, 50, 50), 1000)))
                {
                    return;
                }
                if (Scene.Exists(new Query("Combat/MissionFailed", new Rectangle(1192, 125, 50, 50), 1000)))
                {
                    return;
                }
                if (Scene.Exists(new Query("Combat/Planning", new Rectangle(1, 567, 173, 100))))
                {
                    break;
                }
                if (Scene.Exists(new Query("Combat/EndTurn", new Rectangle(1097, 646, 150, 90))))
                {
                    Program.main.WriteLog("Executing Plan");
                }
                else if (Scene.Exists(new Query("Combat/CombatPaus", new Rectangle(557, 29, 100, 30)))){
                    WaitBattle();
                }
            }
            Thread.Sleep(500);
        }

        protected static void WaitTurn(string turn)
        {
            Mouse.Click(END_ROUND);
            Program.main.WriteLog("Waiting Turn " + turn);
            while (true)
            {
                if (Scene.Exists(new Query("Combat/Turn" + turn)))
                {
                    break;
                }
                if (Scene.Exists(new Query("Combat/MissionSuccess", new Rectangle(1196, 121, 50, 50)), 1000))
                {
                    return;
                }
                if (Scene.Exists(new Query("Combat/MissionFailed", new Rectangle(1192, 125, 50, 50)), 1000))
                {
                    return;
                }
                if (Scene.Exists(new Query("Combat/Terminate", new Rectangle(263, 45, 100, 70)), 1000))
                {
                    Program.main.WriteLog("SF moving");
                }
                if (Scene.Exists(new Query("Combat/CombatPause", new Rectangle(557, 29, 100, 30)), 1000))
                {
                    WaitBattle();
                }
            }
            Program.main.WriteLog("G&K turn started");
            while (true)
            {
                if (Scene.Exists(new Query("Combat/Planning"), 1000, out Point coordinates))
                {
                    Mouse.Click(coordinates);
                    if (Scene.Exists(new Query("Combat/PlanningReady")))
                    {
                        break;
                    }
                }
                if (Scene.Exists(new Query("Combat/MissionFailed", new Rectangle(1192, 125, 50, 50)), 1000))
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
