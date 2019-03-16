﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{

    public enum MissionResult
    {
        Finished,
        RetirementRequired,
        EnhancementRequired
    }

    class Combat
    {
        public static readonly Rectangle ReturnToBase = new Rectangle(11, 45, 117, 70);
        public static readonly Sample CombatScene = new Sample("CombatPage/CombatPage", new Rectangle(375, 63, 191, 58));
        public static readonly Sample CombatMissionClicked = new Sample("CombatPage/CombatMissionClicked", new Rectangle(15, 149, 163, 68));
        public static readonly Button CombatMissionButton = new Button("CombatPage/CombatMission", new Rectangle(15, 149, 163, 68), CombatMissionClicked);

        public static readonly Sample ChapterClickedSample = new Sample("", new Rectangle(225, 146, 155, 588));
        public static readonly Button ChapterButton = new Button("", new Rectangle(225, 146, 130, 596), ChapterClickedSample, .95f, ChapterClickedArea);

        public static readonly Button DollEnhancementButton = new Button("CombatPage/DollEnhancementPopup", new Rectangle(718, 503, 162, 51), Sample.Negative);
        public static readonly Button EquipmentEnhancementButton = new Button("CombatPage/EquipEnhancementPopup", new Rectangle(674, 580, 146, 63), Sample.Negative);

        public static readonly Button NormalBattleButton = new Button("CombatPage/NormalBattle", new Rectangle(674, 580, 146, 63), Sample.Negative);
        public static readonly Button MissionButton = new Button("", new Rectangle(607, 265, 155, 447), NormalBattleButton, .95f, MissionArea);        

        public static readonly Sample Turn0 = new Sample("Combat/Turn0", new Rectangle(416, 66, 71, 292));
        public static readonly Sample SanityCheck = new Sample("", Screen.FullArea);
        
        public static readonly Sample LoadScreenSample = new Sample("LoadScreen", new Rectangle(350, 34, 210, 420));

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
            Thread.Sleep(1000);
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
        }

        public MissionResult ExecuteMission(string mission)
        {            
            var missionTypeButton = new Rectangle(408, 153, 500, 90);
            var parts = mission.Split('_');
            var episode = parts[1];
            if (parts[1].IndexOf('E') != -1)
            {
                screen.Click(missionTypeButton);
                episode = episode.Substring(0, episode.Length - 1);
            }
            else if (parts[1].IndexOf('N') != -1)
            {
                screen.Click(missionTypeButton);
                screen.Click(missionTypeButton);
                episode = episode.Substring(0, episode.Length - 1);
            }
            if (Convert.ToInt32(episode) > 4)
            {
                screen.mouse.DragDownToUp(764, 665, 300);
            }
            MissionButton.Name = "CombatPage/" + mission;
            screen.Click(MissionButton);
            screen.Click(NormalBattleButton);

            if(screen.Exists(DollEnhancementButton, 1000))
            {
                screen.Click(DollEnhancementButton);
                return MissionResult.EnhancementRequired;
            }
            /*
            if (screen.Exists())
            if (Screen.Exists(new Query("CombatPage/DollEnhancementPopup", new Rectangle(779, 508, 30, 30)), 1000, out coords))
            {
                Mouse.Click(coords.X, coords.Y);
                var factory = new Factory();
                factory.DollEnhancement();
                return false;
            }
            if (Screen.Exists(new Query("CombatPage/EquipEnhancementPopup", Window.FullArea), 1000, out coords))
            {
                Mouse.Click(coords.X, coords.Y);
                //EquipEnhancement();
                Program.WriteLog("Equipment full. This is not implemented.");
                Program.Pause();
                return false;
            }
            */
            screen.Interruptible = false;
            screen.Wait(Turn0);            

            var type = Type.GetType("UziTrainer.Chapters.Chapter" + parts[0]);
            Object o = Activator.CreateInstance(type, new object[] { screen, mission });
            var method = type.GetMethod("Map" + mission);
            method.Invoke(o, null);

            screen.Interruptible = true;
            screen.Click(new Rectangle(570, 530, 141, 45), LoadScreenSample, 400);
            return MissionResult.Finished;
        }
    }
}
