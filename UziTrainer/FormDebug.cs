using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Form ClickAreaForm;

        public FormDebug(Screen screen, Sample sample, Point foundAt, float evaluation)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            var location = Program.form.DesktopLocation;
            Location = new Point(location.X + 30, location.Y + 30);
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
                FoundAt = foundAt;
                //FoundAt = sample.RelativeTo(foundAt);
                labelFound.ForeColor = Color.Black;
                labelFound.Text = String.Format("X:{0}, Y:{1}", sample.SearchArea.X + foundAt.X, sample.SearchArea.Y + foundAt.Y);
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
            /*
            var reference = screen.ReferenceRectangle();
            AreaForm = CreateOverlayForm();
            AreaForm.BackColor = Color.Fuchsia;
            AreaForm.DesktopBounds = new Rectangle(new Point(reference.X + sample.SearchArea.X, reference.Y + sample.SearchArea.Y), sample.SearchArea.Size);
            AreaForm.TopMost = true;
            AreaForm.Show();

            if (FoundAt != Point.Empty)
            {
                var pos = sample.AbsolutePosition(FoundAt);
                if (sample is Button)
                {
                    var area = ((Button)sample).ClickArea(pos);
                    ClickAreaForm = CreateOverlayForm();
                    ClickAreaForm.BackColor = Color.Red;
                    ClickAreaForm.DesktopBounds = new Rectangle(new Point(area.X + reference.X, area.Y + reference.Y), area.Size);
                    ClickAreaForm.Show();
                }
                else
                {
                    ClickPoint = CreateOverlayForm();
                    ClickPoint.BackColor = Color.Blue;
                    ClickPoint.DesktopBounds = new Rectangle(new Point(sample.SearchArea.X + reference.X, sample.SearchArea.Y + reference.Y), sample.SearchArea.Size);
                    ClickPoint.Show();
                }                
            }        
            */
            base.Show();
        }

        public new void Close()
        {
            AreaForm.Close();
            if (ClickPoint != null)
            {
                ClickPoint.Close();                
            }
            if (ClickAreaForm != null)
            {
                ClickAreaForm.Close();
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
