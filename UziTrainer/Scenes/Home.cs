﻿using System;
using System.Drawing;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Home
    {
        public static readonly Sample LvSample = new Sample("HomePage/LV", new Rectangle(25, 13, 23, 20));
        public static readonly Button FormationButton = new Button("HomePage/Formation", new Rectangle(899, 401, 153, 65), Formation.FormationScene);
        public static readonly Button CombatButton = new Button("HomePage/Combat", new Rectangle(705, 511, 155, 69), Combat.CombatScene);
        public static readonly Button RepairButton = new Button("HomePage/Repair", new Rectangle(703, 355, 159, 55), Repair.RepairScene);
        public static readonly Sample CriticalDamaged = new Sample("CriticalRepair", new Rectangle(844, 336, 37, 41));

        public static readonly Button LogisticsRepeatButton = new Button("LogisticsRepeat", new Rectangle(568, 501, 118, 45), Sample.Negative);
        public static readonly Sample LogisticsReturned = new Sample("LogisticsReturn", new Rectangle(974, 27, 73, 66), LogisticsRepeatButton);        

        public static readonly Button Event = new Button("Event", new Rectangle(102, 139, 56, 56), Sample.Negative, .95f, EventExit);
        public static readonly Button News = new Button("News", new Rectangle(474, 88, 64, 40), Sample.Negative, .95f, NewsExit);
        public static readonly Button ImportantInformation = new Button("ImportantInformation", new Rectangle(163, 124, 116, 60), Sample.Negative, .95f, ImportantInformationExit);

        private static Rectangle EventExit(Point arg)
        {
            return new Rectangle(29, 142, 58, 33);
        }

        private static Rectangle SignInSuccessExit(Point arg)
        {
            return new Rectangle(481, 524, 121, 37);
        }

        private static Rectangle NewsExit(Point arg)
        {
            return new Rectangle(17, 19, 33, 33);
        }

        private static Rectangle ImportantInformationExit(Point arg)
        {
            return new Rectangle(71, 126, 53, 36);
        }
    }
}
