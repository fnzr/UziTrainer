using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UziTrainer.Window;

namespace UziTrainer.Scenes
{
    class Repair
    {
        public static readonly Rectangle ReturnToBase = new Rectangle(10, 11, 93, 59);
        public static readonly Sample RepairScene = new Sample("RepairPage/RepairPage", new Rectangle(151, 8, 61, 64));
        public static readonly Button QuickRepair = new Button("RepairPage/QuickRepairCheck", new Rectangle(245, 510, 47, 49), null);
        public static readonly Button RepairOK = new Button("RepairPage/RepairOK", new Rectangle(940, 436, 124, 61), QuickRepair);
        public static readonly Button RepairComplete = new Button("RepairPage/RepairComplete", new Rectangle(479, 522, 120, 42), null);

        private Screen screen;
                
        const int RepairSlotX = 90;
        const int RepairSlotXSize = 160;

        const int CriticalRepairX = 110;
        const int CriticalRepairXSize = 150;

        public Repair(Screen screen)
        {
            this.screen = screen;
        }

        private List<Point> FindAvaliableSlots(int max = 6)
        {
            var slots = new List<Point>();
            Button AvailableSlot = new Button("RepairPage/AvailableSlot", Rectangle.Empty, null);
            for (var i = 0; i < max; i++)
            {
                var x = RepairSlotX + (i * RepairSlotXSize);
                AvailableSlot.SearchArea = new Rectangle(x, 330, 70, 80);
                if (screen.Exists(AvailableSlot, 0))
                {
                    slots.Add(new Point(x, 360));
                }
            }
            return slots;
        }

        public void RepairCritical(int max = 4)
        {
            var slots = FindAvaliableSlots(max);
            if (slots.Count == 0)
            {
                Trace.WriteLine("No slots available to repair. Stopping.");
                //Program.Pause();
            }
            screen.Click(new Rectangle(slots[0], new Size(10,10)), RepairOK);
            bool criticalExists = false;
            var CriticalIcon = new Button("RepairPage/CriticalIcon", Rectangle.Empty, null); //TODO
            for (var i = 0; i < slots.Count; i++)
            {
                var x = CriticalRepairX + (i * CriticalRepairXSize);
                CriticalIcon.SearchArea = new Rectangle(x, 110, 40, 40);
                if (screen.Exists(CriticalIcon, 0))
                {
                    screen.Click(new Rectangle(x, 110, 10, 20));
                    criticalExists = true;
                }
            }
            if (criticalExists)
            {
                screen.Click(RepairOK);
                screen.Click(QuickRepair);
                screen.Click(new Rectangle(720, 508, 118, 44), RepairComplete);
                screen.Click(RepairComplete);
            }
            else
            {
                screen.Click(new Rectangle(10, 14, 93, 53));
            }
        }
    }
}
