using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UziTrainer.Window;
using Button = UziTrainer.Window.Button;
using Screen = UziTrainer.Window.Screen;

namespace UziTrainer
{
    public partial class FormDebug : Form
    {
        Screen screen;
        Sample sample;
        Point FoundAt;
        Form AreaForm;
        Form ClickPoint;
        Form ClickArea;

        public FormDebug(Screen screen, Sample sample, Point foundAt, float evaluation)
        {
            InitializeComponent();
            this.screen = screen;
            this.sample = sample;

            labelSearch.Text = sample.Name;
            labelArea.Text = sample.SearchArea.ToString();            
            labelEval.Text = evaluation.ToString();
            if (foundAt == Point.Empty)
            {
                labelFound.ForeColor = Color.Red;
                labelFound.Text = "Not Found";
            }
            else
            {
                FoundAt = sample.RelativeTo(foundAt);
                labelFound.ForeColor = Color.Black;
                labelFound.Text = foundAt.ToString();
            }
        }

        Form CreateOverlayForm()
        {
            var form = new Form();
            form.ShowInTaskbar = false;
            form.Opacity = .3f;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;
            form.TopMost = true;
            var initialStyle = Win32.Message.GetWindowLongPtr(form.Handle, -20).ToInt32();
            Win32.Message.SetWindowLongPtr(form.Handle, -20, new IntPtr(initialStyle | 0x80000 | 0x20 | 0x80));
            return form;
        }

        public new void Show()
        {
            var reference = screen.ReferenceRectangle();
            AreaForm = CreateOverlayForm();
            AreaForm.BackColor = Color.Fuchsia;
            AreaForm.DesktopBounds = new Rectangle(reference.Left + sample.SearchArea.Left, reference.Top + sample.SearchArea.Top, sample.SearchArea.Width, sample.SearchArea.Height);
            AreaForm.Show();

            if (FoundAt != Point.Empty)
            {
                ClickPoint = CreateOverlayForm();
                ClickPoint.BackColor = Color.Green;
                ClickPoint.DesktopBounds = new Rectangle(reference.Left + (FoundAt.X - 10), reference.Top + (FoundAt.Y - 10), 20, 20);
                ClickPoint.Show();

                if (sample is Button)
                {
                    var area = ((Button)sample).ClickArea(FoundAt);
                    ClickArea = CreateOverlayForm();
                    ClickArea.BackColor = Color.Blue;
                    ClickArea.DesktopBounds = new Rectangle(reference.Left + area.Left, reference.Top + area.Top, area.Width, area.Height);
                    ClickArea.Show();
                }
            }            
            base.Show();
        }

        public new void Close()
        {
            AreaForm.Close();
            if (ClickPoint != null)
            {
                ClickPoint.Close();                
            }
            if (ClickArea != null)
            {
                ClickArea.Close();
            }
            base.Close();
            Program.DebugResetEvent.Set();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {            
            Close();            
        }
    }
}
