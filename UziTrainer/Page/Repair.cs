using System.Collections.Generic;
using System.Drawing;
namespace UziTrainer.Page
{
    class Repair
    {
        const int RepairSlotX = 118;
        const int RepairSlotXSize = 180;

        const int CriticalRepairX = 120;
        const int CriticalReapairXSize = 180;

        private static List<Point> FindAvaliableSlots(int max = 6)
        {
            var slots = new List<Point>();
            for (var i = 0; i < max; i++)
            {
                var x = RepairSlotX + (i * RepairSlotXSize);
                if (Scene.Exists(new Query("RepairPage/AvailableSlot", new Rectangle(x, 350, 50, 50)), 100))
                {
                    slots.Add(new Point(x, 320));
                }
            }
            return slots;
        }

        public static void RepairCritical(int max = 6)
        {
            var slots = FindAvaliableSlots(max);
            if (slots.Count == 0)
            {
                Program.main.WriteLog("No slots available to repair. Stopping.");
                Program.Pause();
            }
            Mouse.Click(slots[0]);
            Scene.Wait(new Query("RepairPage/RepairOK"));
            for (var i = 0; i < slots.Count; i++)
            {
                var x = CriticalRepairX + (i * CriticalReapairXSize);
                if (Scene.Exists(new Query("RepairPage/CriticalIcon", new Rectangle(x, 160, 80, 80)), 500, out Point coords))
                {
                    Mouse.Click(coords);
                }
            }
            Scene.Click(new Query("RepairPage/RepairOK", new Rectangle(1178, 559, 50, 50)));
            Scene.Click(new Query("RepairPage/QuickRepairCheck", new Rectangle(297, 517, 30, 30)));
            Mouse.Click(923, 536);
            Scene.Click(new Query("RepairPage/RepairComplete", new Rectangle(576, 533, 40, 40)));
            Scene.Transition(Scene.RepairQuery, Scene.HomeQuery, new Point(71, 71));
        }
    }
}
