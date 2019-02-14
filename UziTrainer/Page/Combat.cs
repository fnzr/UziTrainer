using System;
using System.Drawing;

namespace UziTrainer.Page
{
    class Combat
    {
        public Combat()
        {
        }

        public void Setup(string mission)
        {
            if (!Screen.Exists(new Query("CombatPage/CombatMissionClicked", new Rectangle(9,139,170,80)), 2000)) {
                Screen.Transition(new Query("CombatPage/CombatMission", new Rectangle(9, 139, 170, 80)), new Query("CombatPage/CombatMissionClicked", new Rectangle(9, 139, 170, 80)));
            }
            var parts = mission.Split('_');
            int chapter = Convert.ToInt32(parts[0]);
            int map = Convert.ToInt32(parts[1]);

            if (chapter > 5)
            {
                Mouse.DragDownToUp(264, 689, 247);
            }
            var chapterClickedQuery = new Query("CombatPage/Chapter" + parts[0] + "Clicked", new Rectangle(180, 119, 250, 624));
            if (!Screen.Exists(chapterClickedQuery)) {
                Screen.Transition(new Query("CombatPage/Chapter" + parts[0] + "NotClicked", new Rectangle(193, 119, 218, 624)), chapterClickedQuery);
            }
            var success = Execute(mission);
            if (success)
            {
                Screen.Transition(new Query("CombatPage/Return"), Screen.HomeQuery, new RPoint(71, 79));
            }
            else
            {
                Screen.Transition(Screen.HomeQuery, Screen.CombatQuery, new RPoint(930, 500));
                Setup(mission);
            }            
        }

        public bool Execute(string mission)
        {
            if (mission.IndexOf('E') != -1)
            {
                Mouse.Click(640, 195);
            }
            else if (mission.IndexOf('N') != -1)
            {
                Mouse.Click(640, 195);
                Mouse.Click(640, 195);
            }            
            if(Convert.ToInt32(mission.Split('_')[1]) > 4)
            {
                Mouse.DragDownToUp(764, 665, 300);
            }
            Screen.Transition(new Query("CombatPage/" + mission, new Rectangle(639, 265, 70, 404)), new Query("CombatPage/NormalBattle", new Rectangle(734, 601, 70, 30)));
            Screen.Click(new Query("CombatPage/NormalBattle", new Rectangle(734,601,70,30)));

            Point coords;
            if (Screen.Exists(new Query("CombatPage/DollEnhancementPopup", new Rectangle(779,508,30,30)), 1000, out coords)) {
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
            Screen.Wait(new Query("Combat/Turn0", new Rectangle(401, 3, 90, 120)));            

            if (!Screen.Exists(new Query("Missions/" + mission + "/SanityCheck", Window.FullArea), 1000, out _)){
                Mouse.ZoomOut();
            }

            var parts = mission.Split('_');

            var type = Type.GetType("UziTrainer.Chapters.Chapter" + parts[0]);
            Object o = Activator.CreateInstance(type, null);
            var method = type.GetMethod("Map" + mission);
            method.Invoke(o, null);
            
            Screen.ClickUntilFound(new Query("LoadScreen", new Rectangle(350, 34, 210, 420)), new RPoint(356, 652));
            return true;
        }
    }
}
