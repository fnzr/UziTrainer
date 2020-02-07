using System;
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
        public static readonly Rectangle ReturnToBase = new Rectangle(12, 12, 93, 57);
        public static readonly Sample CombatScene = new Sample("CombatPage/CombatPage", new Rectangle(317, 27, 115, 40));
        public static readonly Sample CombatMissionClicked = new Sample("CombatPage/CombatMissionClicked", new Rectangle(12, 97, 134, 59));
        public static readonly Button CombatMissionButton = new Button("CombatPage/CombatMission", new Rectangle(12, 97, 134, 59), CombatMissionClicked);

        public static readonly Sample ChapterClickedSample = new Sample("", new Rectangle(172, 91, 165, 713));
        public static readonly Button ChapterButton = new Button("", new Rectangle(172, 91, 165, 713), ChapterClickedSample, .95f, ChapterClickedArea);

        public static readonly Button DollEnhancementButton = new Button("CombatPage/DollEnhancementPopup", new Rectangle(610, 501, 136, 42), Sample.Negative);
        public static readonly Button EquipmentEnhancementButton = new Button("CombatPage/EquipEnhancementPopup", new Rectangle(674, 580, 146, 63), Sample.Negative, .94f);

        public static readonly Button NormalBattleButton = new Button("CombatPage/NormalBattle", new Rectangle(573, 571, 114, 44), Sample.Negative);
        public static readonly Button MissionButton = new Button("", new Rectangle(531, 196, 82, 565), NormalBattleButton, .95f, MissionArea);

        public static readonly Sample Turn0 = new Sample("Combat/Turn0", new Rectangle(357, 37, 49, 38));
        public static readonly Sample SanityCheck = new Sample("", Screen.FullArea, null);

        public static readonly Sample LoadScreenSample = new Sample("LoadScreen", new Rectangle(333, 385, 100, 80), null, .8f);

        private Screen screen;

        private static Rectangle MissionArea(Point arg)
        {
            int mission = (arg.Y - 200) / 100;
            int y = 200 + (100 * mission);
            return new Rectangle(370, arg.Y - 10, 500, 70);
        }

        private static Rectangle ChapterClickedArea(Point arg)
        {
            int chapter = (arg.Y - 90) / 90;
            int y = 90 + (87 * chapter);
            return new Rectangle(170, y, 100, 65);
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
            ChapterClickedSample.Name = $"CombatPage/Chapter{parts[0]}Clicked";
            if (!screen.Exists(ChapterClickedSample))
            {
                ChapterButton.Name = $"CombatPage/Chapter{parts[0]}";
                if (!screen.Exists(ChapterButton, 0))
                {
                    if (chapter < 4)
                    {
                        screen.mouse.DragUpToDown(264, 247, 689);
                    }
                    else
                    {
                        screen.mouse.DragDownToUp(264, 689, 247);
                    }
                }
                screen.Click(ChapterButton);
            }
        }        

        public MissionResult ExecuteMission(string mission)
        {
            var missionTypeButton = new Rectangle(356, 100, 412, 81);
            var parts = mission.Split('_');
            var episode = parts[1];
            Rectangle difficultyArea = new Rectangle(861, 81, 44, 33);
            Rectangle clickArea;
            Sample difficultySample;
            if (parts[1].IndexOf('E') != -1)
            {
                difficultySample = new Sample("CombatPage/DifficultyEmergency", difficultyArea);
                clickArea = new Rectangle(900, 144, 67, 25);
                episode = episode.Substring(0, episode.Length - 1);
            }
            else if (parts[1].IndexOf('N') != -1)
            {
                difficultySample = new Sample("CombatPage/DifficultyNight", difficultyArea);
                clickArea = new Rectangle(1009, 150, 40, 17);
                episode = episode.Substring(0, episode.Length - 1);
            }            
            else
            {
                difficultySample = new Sample("CombatPage/DifficultyNormal", difficultyArea);
                clickArea = new Rectangle(807, 138, 59, 35);
            }
            if (!screen.Exists(difficultySample))
            {
                screen.Click(clickArea, difficultySample, 0);
            }
            MissionButton.Name = "CombatPage/" + mission;
            screen.Click(MissionButton);
            screen.Click(NormalBattleButton);

            
            if(screen.Exists(DollEnhancementButton, 1000))
            {
                screen.Click(DollEnhancementButton);
                return MissionResult.EnhancementRequired;
            }
            
            screen.Interruptible = false;
            screen.Wait(Turn0);            

            var type = Type.GetType("UziTrainer.Chapters.Chapter" + parts[0]);
            Object o = Activator.CreateInstance(type, new object[] { screen, mission });
            var method = type.GetMethod("Map" + mission);
            method.Invoke(o, null);

            screen.Interruptible = true;
            screen.Click(new Rectangle(481, 523, 117, 39), LoadScreenSample, 1000);
            return MissionResult.Finished;
        }

        public bool StartMission(string mission)
        {
            if (!screen.Exists(CombatMissionClicked, 2000))
            {
                screen.Click(CombatMissionButton);
            }
            var parts = mission.Split('_');
            int chapter = Convert.ToInt32(parts[0]);
            Thread.Sleep(1000);
            ChapterClickedSample.Name = $"CombatPage/Chapter{parts[0]}Clicked";
            if (!screen.Exists(ChapterClickedSample))
            {
                ChapterButton.Name = $"CombatPage/Chapter{parts[0]}";
                if (!screen.Exists(ChapterButton, 0))
                {
                    if (chapter < 4)
                    {
                        screen.mouse.DragUpToDown(264, 247, 689);
                    }
                    else
                    {
                        screen.mouse.DragDownToUp(264, 689, 247);
                    }
                }
                screen.Click(ChapterButton);
            }

            var missionTypeButton = new Rectangle(356, 100, 412, 81);
            
            var episode = parts[1];
            Rectangle difficultyArea = new Rectangle(861, 81, 44, 33);
            Rectangle clickArea;
            Sample difficultySample;
            if (parts[1].IndexOf('E') != -1)
            {
                difficultySample = new Sample("CombatPage/DifficultyEmergency", difficultyArea);
                clickArea = new Rectangle(900, 144, 67, 25);
                episode = episode.Substring(0, episode.Length - 1);
            }
            else if (parts[1].IndexOf('N') != -1)
            {
                difficultySample = new Sample("CombatPage/DifficultyNight", difficultyArea);
                clickArea = new Rectangle(1009, 150, 40, 17);
                episode = episode.Substring(0, episode.Length - 1);
            }
            else
            {
                difficultySample = new Sample("CombatPage/DifficultyNormal", difficultyArea);
                clickArea = new Rectangle(807, 138, 59, 35);
            }
            if (!screen.Exists(difficultySample))
            {
                screen.Click(clickArea, difficultySample, 0);
            }
            MissionButton.Name = "CombatPage/" + mission;
            screen.Click(MissionButton);
            screen.Click(NormalBattleButton);


            if (screen.Exists(DollEnhancementButton, 1000))
            {
                screen.Click(DollEnhancementButton);
                return false;
            }

            screen.Interruptible = false;
            screen.Wait(Turn0);
            
            return true;
        }
    }
}
