using System;
using System.Drawing;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Home
    {
        public static readonly Sample LvSample = new Sample("HomePage/LV", new Rectangle(33, 49, 24, 19));
        public static readonly Button FormationButton = new Button("HomePage/Formation", new Rectangle(1064, 446, 194, 77), Formation.FormationScene);
        public static readonly Button CombatButton = new Button("HomePage/Combat", new Rectangle(835, 460, 185, 76), Combat.CombatScene);
        public static readonly Button RepairButton = new Button("HomePage/Combat", new Rectangle(832, 273, 183, 61), Repair.RepairScene);
        public static readonly Sample CriticalDamaged = new Sample("CriticalRepair", new Rectangle(1007, 264, 25, 25));

        public static readonly Sample LogisticsReturned = new Sample("LogisticsReturn", new Rectangle(1248, 32, 29, 26));
        public static readonly Button LogisticsRepeatButton = new Button("LogisticsRepeat", new Rectangle(678, 509, 132, 43), Sample.Negative);
    }
}
