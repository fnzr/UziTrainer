using System.Drawing;
using System.Threading;

namespace UziTrainer.Chapters
{
    class Chapter0 : Chapter
    {

        public static void Map0_2()
        {
            Mouse.Click(659, 386);
            Scene.Click(DEPLOY_ECHELON);
            Thread.Sleep(500);
            Mouse.Click(262, 374);
            Scene.Click(DEPLOY_ECHELON);
            Mouse.Click(START_OPERATION);
            Thread.Sleep(2000);

            Scene.Transition(new Query("Missions/0_2/SanityCheck"), RESUPPLY, new Point(263, 381));
            Scene.Click(RESUPPLY);
            Thread.Sleep(500);
            Scene.Transition(new Query("Combat/Planning"), new Query("Combat/PlanningReady"));
            Mouse.Click(659, 386);
            Mouse.Click(501, 289);
            Mouse.DragUpToDown(700, 104, 734);
            Scene.ClickUntilFound(new Query("Missions/0_2/Plan2", new Rectangle(494, 621, 25, 25)), new Point(535, 591));
            Mouse.Click(671, 387);
            Mouse.Click(523, 290);
            WaitExecution();
            WaitTurn("2");

            Mouse.Click(524, 293);
            Mouse.Click(811, 288);
            Mouse.Click(1000, 329);
            WaitExecution();
            Mouse.Click(END_ROUND);
        }

    }
}
