using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Combat
    {
        public static readonly Sample CombatScene = new Sample("CombatPage/CombatPage", new Rectangle(375, 63, 191, 58));
        public static readonly Sample CombatMissionClicked = new Sample("CombatPage/CombatMissionClicked", new Rectangle(15, 149, 163, 68));
        public static readonly Button CombatMissionButton = new Button("CombatPage/CombatMission", new Rectangle(15, 149, 163, 68), CombatMissionClicked);

        public static readonly Sample ChapterClickedSample = new Sample("", new Rectangle(225, 146, 155, 588));
        public static readonly Button ChapterButton = new Button("", new Rectangle(225, 146, 130, 596), ChapterClickedSample, .95f, ChapterClickedArea);

        private static Rectangle ChapterClickedArea(Point arg)
        {
            int chapter = (arg.Y - 145) / 90;
            int y = 145 + (90 * chapter);
            return new Rectangle(210, y, 120, 65);
        }
    }
}
