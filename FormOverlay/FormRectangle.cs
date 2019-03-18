using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormOverlay
{
    public partial class FormRectangle : Form
    {
        Point Down;

        public FormRectangle()
        {
            var screen = new UziTrainer.Window.Screen("");
            InitializeComponent();
            ShowInTaskbar = false;
            Opacity = .3f;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.White;
            TopMost = true;
            var initialStyle = UziTrainer.Win32.Message.GetWindowLongPtr(Handle, -20).ToInt32();
            UziTrainer.Win32.Message.SetWindowLongPtr(Handle, -20, new IntPtr(initialStyle | 0x80000 | 0x80));
            var rect = screen.ReferenceRectangle();
            SetDesktopBounds(rect.X, rect.Y, rect.Width, rect.Height);

            MouseDown += FormRectangle_MouseDown;
            MouseUp += FormRectangle_MouseUp;
        }

        private void FormRectangle_MouseUp(object sender, MouseEventArgs e)
        {
            var loc = DesktopLocation;
            System.IO.File.WriteAllText(@"./out.txt", $"{loc.X + Down.X}, {loc.Y + Down.Y}, {e.Location.X - Down.X}, {e.Location.Y - Down.Y}");

        }

        private void FormRectangle_MouseDown(object sender, MouseEventArgs e)
        {            
            Down = e.Location;
        }
    }
}
