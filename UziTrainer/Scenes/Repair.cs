using System;
using System.Collections.Generic;
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
        private Screen screen;

        public Repair(Screen screen)
        {
            this.screen = screen;
        }

        internal void RepairCritical()
        {
            //throw new NotImplementedException();
        }
    }
}
