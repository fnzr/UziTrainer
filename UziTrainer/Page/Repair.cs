using System;
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

        public Repair()
        {
        }

        private List<Point> FindAvaliableSlots(int max = 6)
        {
            var slots = new List<Point>();
            for (var i = 0; i < max; i++)
            {
                var x = RepairSlotX + (i * RepairSlotXSize);
                if (Screen.Exists(new Query("RepairPage/AvailableSlot", new Rectangle(x, 325, 50, 50)), 500))
                {
                    slots.Add(new Point(x, 320));
                }
            }
            return slots;
        }

        public void RepairCritical(int max = 6)
        {
            var slots = FindAvaliableSlots(max);
            if (slots.Count == 0)
            {
                Program.WriteLog("No slots available to repair. Stopping.");
                Program.Pause();
            }
            Mouse.Click(slots[0]);
            Screen.Wait(new Query("RepairPage/RepairOK"));
            bool criticalExists = false;
            for (var i = 0; i < slots.Count; i++)
            {
                var x = CriticalRepairX + (i * CriticalReapairXSize);
                if (Screen.Exists(new Query("RepairPage/CriticalIcon", new Rectangle(x, 160, 80, 80)), 500, out Point coords))
                {
                    Mouse.Click(coords);
                    criticalExists = true;
                }
            }
            if (criticalExists)
            {
                Screen.Click(new Query("RepairPage/RepairOK", new Rectangle(1178, 559, 50, 50)));
                Screen.Click(new Query("RepairPage/QuickRepairCheck", new Rectangle(297, 517, 30, 30)));
                Screen.Click(new Point(923, 536));
                Screen.Click(new Query("RepairPage/RepairComplete", new Rectangle(576, 533, 40, 40)));
            }
            else
            {
                Screen.Click(new Point(68, 86));
            }
            Screen.Transition(Screen.RepairQuery, Screen.HomeQuery, new RPoint(71, 71));
        }
    }
}
