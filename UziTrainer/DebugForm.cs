using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UziTrainer
{
    class DebugForm : Form
    {
        private static DebugForm ClickForm = new DebugForm();
        private static DebugForm SearchForm = new DebugForm();
        public static Rectangle Reference;
        public DebugForm()
        {            
            ShowInTaskbar = false;
            Opacity = .3f;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;            
            //DesktopBounds = new Rectangle(referenceArea.Left + area.Left, referenceArea.Top + area.Top, area.Width, area.Height);
            TopMost = true;
            var initialStyle = Message.GetWindowLongPtr(Handle, -20).ToInt32();
            Message.SetWindowLongPtr(Handle, -20, new IntPtr(initialStyle | 0x80000 | 0x20 | 0x80));
        }

        public static void DebugClick(int x, int y)
        {
            ClickForm.BackColor = Color.Green;
            ClickForm.DesktopBounds = new Rectangle(Reference.Left + (x - 10), Reference.Top + (y - 10), 20, 20);            
            ClickForm.Show();
        }

        public static void DebugSearch(Rectangle area)
        {
            SearchForm.BackColor = Color.Fuchsia;
            SearchForm.DesktopBounds = new Rectangle(Reference.Left + area.Left, Reference.Top + area.Top, area.Width, area.Height);
            SearchForm.Show();
        }
    }
}
