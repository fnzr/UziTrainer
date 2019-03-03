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
        public static readonly Rectangle ReturnToBase = new Rectangle(13, 48, 110, 69);
        public static readonly Sample RepairScene = new Sample("RepairPage/RepairPage", new Rectangle(195, 73, 25, 25));
        public static readonly Button QuickRepair = new Button("RepairPage/QuickRepairCheck", new Rectangle(297, 517, 30, 30), null);
        public static readonly Button RepairOK = new Button("RepairPage/RepairOK", new Rectangle(1178, 559, 50, 50), QuickRepair);
        public static readonly Button RepairComplete = new Button("RepairPage/RepairComplete", new Rectangle(576, 533, 40, 40), null);

        private Screen screen;
                
        const int RepairSlotX = 118;
        const int RepairSlotXSize = 180;

        const int CriticalRepairX = 120;
        const int CriticalReapairXSize = 180;

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
                AvailableSlot.SearchArea = new Rectangle(x, 325, 50, 50);
                if (screen.Exists(AvailableSlot, 500))
                {
                    slots.Add(new Point(x, 320));
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
                var x = CriticalRepairX + (i * CriticalReapairXSize);
                CriticalIcon.SearchArea = new Rectangle(x, 160, 80, 80);
                if (screen.Exists(CriticalIcon, 0))
                {
                    screen.Click(CriticalIcon.SearchArea);
                    criticalExists = true;
                }
            }
            if (criticalExists)
            {
                screen.Click(RepairOK);
                screen.Click(QuickRepair);
                screen.Click(new Rectangle(850, 515, 140, 47), RepairComplete);
                screen.Click(RepairComplete);
            }
            else
            {
                screen.Click(new Rectangle(13, 50, 106, 55));
            }
        }
    }
}
