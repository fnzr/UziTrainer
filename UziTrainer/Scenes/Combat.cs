﻿using System;
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

        public static readonly Button NormalBattleButton = new Button("CombatPage/NormalBattle", new Rectangle(674, 580, 146, 63), Sample.Negative);
        public static readonly Button MissionButton = new Button("", new Rectangle(607, 265, 155, 447), NormalBattleButton, .95f, MissionArea);

        public static readonly Button ReturnButton = new Button("CombatPage/Return", new Rectangle(11, 45, 117, 70), Sample.Negative);
        private Screen screen;

        private static Rectangle MissionArea(Point arg)
        {
            int mission = (arg.Y - 265) / 115;
            int y = 270 + (115 * mission);
            return new Rectangle(430, arg.Y - 10, 810, 70);
        }

        private static Rectangle ChapterClickedArea(Point arg)
        {
            int chapter = (arg.Y - 145) / 90;
            int y = 145 + (90 * chapter);
            return new Rectangle(210, y, 120, 65);
        }

        public Combat(Screen screen)
        {
            this.screen = screen;
        }

        public void PrepareMission(string mission)
        {
            if (!screen.Exists(CombatMissionClicked, 2000))
            {
                screen.Click(CombatMissionButton);
            }
            var parts = mission.Split('_');
            int chapter = Convert.ToInt32(parts[0]);
            int map = Convert.ToInt32(parts[1]);

            if (chapter > 5)
            {
                screen.mouse.DragDownToUp(264, 689, 247);
            }
            ChapterClickedSample.Name = $"CombatPage/Chapter{parts[0]}Clicked";
            if (!screen.Exists(ChapterClickedSample))
            {
                ChapterButton.Name = $"CombatPage/Chapter{parts[0]}";
                screen.Click(ChapterButton);
            }
            var success = ExecuteMission(mission);
            if (success)
            {
                screen.Click(ReturnButton);
            }
            else
            {
                screen.Click(Home.CombatButton);
                ExecuteMission(mission);
            }
        }

        public bool ExecuteMission(string mission)
        {
            return true;
        }
    }
}
