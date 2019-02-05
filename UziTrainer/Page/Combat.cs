using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer.Page
{
    class Combat
    {
        public static void Setup(string mission)
        {
            if (!Scene.Exists(new Query("CombatPage/CombatMissionClicked", new Rectangle(9,139,170,80)), 2000)) {
                Scene.Transition(new Query("CombatPage/CombatMission", new Rectangle(9, 139, 170, 80)), new Query("CombatPage/CombatMissionClicked", new Rectangle(9, 139, 170, 80)));
            }
            var parts = mission.Split('_');
            int chapter = Convert.ToInt32(parts[0]);
            int map = Convert.ToInt32(parts[1]);

            if (chapter > 5)
            {
                Mouse.DragDownToUp(264, 689, 247);
            }
            var chapterClickedQuery = new Query("CombatPage/Chapter" + parts[0] + "Clicked", new Rectangle(180, 119, 250, 624));
            if (!Scene.Exists(chapterClickedQuery)) {
                Scene.Transition(new Query("CombatPage/Chapter" + parts[0] + "NotClicked", new Rectangle(193, 119, 218, 624)), chapterClickedQuery);
            }
            if (Execute(mission))
            {
                Scene.Transition(new Query("CombatPage/Return"), Scene.HomeQuery, new Point(71, 79));
            }
            else
            {
                Scene.Transition(Scene.HomeQuery, Scene.CombatQuery, new Point(930, 500));
                Setup(mission);
            }
        }

        public static bool Execute(string mission)
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
            Scene.Transition(new Query("CombatPage/" + mission, new Rectangle(639, 265, 70, 404)), new Query("CombatPage/NormalBattle", new Rectangle(734, 601, 70, 30)));
            Scene.Click(new Query("CombatPage/NormalBattle", new Rectangle(734,601,70,30)));

            Point coords;
            if (Scene.Exists(new Query("CombatPage/DollEnhancementPopup", new Rectangle(779,508,30,30)), 1000, out coords)) {
                Mouse.Click(coords.X, coords.Y);
                Factory.DollEnhancement();
                return false;
            }
            if (Scene.Exists(new Query("CombatPage/EquipEnhancementPopup", Window.FullArea), 1000, out coords))
            {
                Mouse.Click(coords.X, coords.Y);
                //EquipEnhancement();
                Program.main.WriteLog("Equipment full. This is not implemented.");
                Program.Pause();
                return false;
            }
            Scene.Wait(new Query("Combat/Turn0", new Rectangle(401, 3, 90, 120)));            

            if (!Scene.Exists(new Query("Missions/" + mission + "/SanityCheck", Window.FullArea), 1000, out _)){
                Mouse.ZoomOut();
            }

            var parts = mission.Split('_');

            var type = Type.GetType("UziTrainer.Chapters.Chapter" + parts[0]);
            var method = type.GetMethod("Map" + mission);
            method.Invoke(new object(), null);
            
            Scene.ClickUntilFound(new Query("LoadScreen", new Rectangle(350, 34, 210, 420)), new Point(356, 652));
            return true;
        }
    }
}
