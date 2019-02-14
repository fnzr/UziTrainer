using System.Drawing;
using System.Threading;

namespace UziTrainer.Chapters
{
    class Chapter0 : Chapter
    {
        public Chapter0() : base()
        {
        }

        public void Map0_2()
        {
            Mouse.Click(659, 386);
            Screen.Click(DeployEchelon);
            Thread.Sleep(500);
            Mouse.Click(262, 374);
            Screen.Click(DeployEchelon);
            Mouse.Click(StartOperationN);
            Thread.Sleep(2000);
            Screen.Transition(new Query("Missions/0_2/SanityCheck"), Resupply, new RPoint(263, 381));
            Screen.Click(Resupply);
            Thread.Sleep(500);
            Screen.Transition(new Query("Combat/Planning"), new Query("Combat/PlanningReady"));
            Mouse.Click(659, 386);
            Mouse.Click(501, 289);
            Mouse.DragUpToDown(700, 104, 734);
            Screen.ClickUntilFound(new Query("Missions/0_2/Plan2", new Rectangle(494, 621, 25, 25)), new RPoint(535, 591));
            Mouse.Click(671, 387);
            Mouse.Click(523, 290);
            WaitExecution();
            WaitTurn("2");

            Mouse.Click(524, 293);
            Mouse.Click(811, 288);
            Mouse.Click(1000, 329);
            WaitExecution();
            Mouse.Click(EndRound);
        }

    }
}
