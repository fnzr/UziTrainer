using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UziTrainer
{
    class ImageSearchForm : Form
    {
        private Label label2;
        private Label label3;
        private Label labelSearch;
        private Label labelArea;
        private Label labelFound;
        private Label label1;
        private Button buttonContinue;

        public ManualResetEvent DebugThread = new ManualResetEvent(false);
        private List<Form> debugForms = new List<Form>();

        public ImageSearchForm(Query query)
        {
            InitializeComponent();
            labelArea.Text = query.Area.ToString();
            labelSearch.Text = query.ImagePath;            
        }

        public void SetFoundAt(Point position)
        {
            if (position == Point.Empty)
            {
                labelFound.ForeColor = Color.Red;
                labelFound.Text = "Not Found";
            }
            else
            {
                labelFound.ForeColor = Color.Black;
                labelFound.Text = position.ToString();
            }
        }

        private Form CreateDebugForm()
        {
            var form = new Form();
            form.ShowInTaskbar = false;
            form.Opacity = .3f;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;
            form.TopMost = true;
            var initialStyle = Message.GetWindowLongPtr(form.Handle, -20).ToInt32();
            Message.SetWindowLongPtr(form.Handle, -20, new IntPtr(initialStyle | 0x80000 | 0x20 | 0x80));

            debugForms.Add(form);
            return form;
        }

        public new void Dispose()
        {
            foreach (var form in this.debugForms)
            {
                //this.BeginInvoke((Action)(() => form.Dispose()));
                form.Dispose();
            }
            //this.BeginInvoke((Action)(() => base.Dispose()));
            base.Dispose();
        }

        public void DebugClick(int x, int y)
        {
            var reference = Window.WindowRectangle;
            var ClickForm = CreateDebugForm();
            ClickForm.BackColor = Color.Green;
            ClickForm.DesktopBounds = new Rectangle(reference.Left + (x - 10), reference.Top + (y - 10), 20, 20);            
            ClickForm.Show();
        }

        public void DebugArea(Rectangle area, Color color)
        {
            var reference = Window.WindowRectangle;
            var SearchForm = CreateDebugForm();
            SearchForm.BackColor = color;
            SearchForm.DesktopBounds = new Rectangle(reference.Left + area.Left, reference.Top + area.Top, area.Width, area.Height);
            SearchForm.Show();
            var x = SearchForm.Handle;
        }

        public new void Close()
        {
            foreach (var form in this.debugForms)
            {
                form.Close();
            }
            base.Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            Close();
            DebugThread.Set();            
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelArea = new System.Windows.Forms.Label();
            this.labelFound = new System.Windows.Forms.Label();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Area";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Found";
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(93, 13);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(63, 13);
            this.labelSearch.TabIndex = 3;
            this.labelSearch.Text = "labelSearch";
            // 
            // labelArea
            // 
            this.labelArea.AutoSize = true;
            this.labelArea.Location = new System.Drawing.Point(93, 36);
            this.labelArea.Name = "labelArea";
            this.labelArea.Size = new System.Drawing.Size(51, 13);
            this.labelArea.TabIndex = 4;
            this.labelArea.Text = "labelArea";
            // 
            // labelFound
            // 
            this.labelFound.AutoSize = true;
            this.labelFound.Location = new System.Drawing.Point(93, 58);
            this.labelFound.Name = "labelFound";
            this.labelFound.Size = new System.Drawing.Size(59, 13);
            this.labelFound.TabIndex = 5;
            this.labelFound.Text = "labelFound";
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(197, 58);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 6;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // ImageSearchForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 93);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.labelFound);
            this.Controls.Add(this.labelArea);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ImageSearchForm";
            this.Text = "Search Debug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
