using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UziTrainer
{    
    public partial class FormImageCreator : Form
    {
        bool Drawing = false;
        readonly double ImageScale = 0.75;
        Bitmap OriginalImage;
        readonly DrawArea TemplateArea = new DrawArea(Color.FromArgb(80, Color.Red), Rectangle.Empty);
        readonly DrawArea SearchArea = new DrawArea(Color.FromArgb(80, Color.Blue), Rectangle.Empty);
        readonly DrawArea ClickArea = new DrawArea(Color.FromArgb(80, Color.Green), Rectangle.Empty);
        DrawArea ActiveArea;

        List<Button> ButtonList;

        Point Origin;

        public FormImageCreator(Samples sample) : this()
        {
            comboBoxName.SelectedItem = sample;
        }

        public FormImageCreator(Template template) : this()
        {
            comboBoxName.SelectedItem = template.Name;
            comboBoxNext.SelectedItem = template.Next;

            SearchArea.Area = ConvertRectangle(template.SearchArea, true);
            ClickArea.Area = ConvertRectangle(template.ClickArea, true);
        }
        public FormImageCreator()
        {
            InitializeComponent();            

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseMove += PictureBox_MouseMove;

            pictureBox.Paint += PictureBox_Paint;

            labelSearch.ForeColor = SearchArea.Color;
            labelTemplate.ForeColor = TemplateArea.Color;
            labelClick.ForeColor = ClickArea.Color;

            ButtonList = new List<Button>()
            {
                buttonClick, buttonSearch, buttonTemplateArea
            };

            buttonClick.Click += Button_Click;
            buttonSearch.Click += Button_Click;
            buttonTemplateArea.Click += Button_Click;

            buttonClick.Tag = ClickArea;
            buttonTemplateArea.Tag = TemplateArea;
            buttonSearch.Tag = SearchArea;

            ButtonRefresh_Click(null, null);

            Button_Click(buttonTemplateArea, null);
            Refresh();

            buttonDone.Click += ButtonDone_Click;

            buttonRefresh.Click += ButtonRefresh_Click;            

            comboBoxName.DataSource = Enum.GetValues(typeof(Samples));
            comboBoxNext.DataSource = Enum.GetValues(typeof(Samples));
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            var ss = Android.Screenshot();
            OriginalImage = ss.Bitmap;
            pictureBox.Image = ss.Resize(ImageScale, Emgu.CV.CvEnum.Inter.Linear).Bitmap;
        }

        private Rectangle ConvertRectangle(Rectangle original, bool shrink = false)
        {
            Rectangle rect;
            if (shrink)
            {
                rect = new Rectangle
                {
                    X = (int)(original.X * ImageScale),
                    Y = (int)(original.Y * ImageScale),
                    Width = (int)(original.Width * ImageScale),
                    Height = (int)(original.Height * ImageScale)
                };
            }
            else
            {
                rect = new Rectangle
                {
                    X = (int)(original.X / ImageScale),
                    Y = (int)(original.Y / ImageScale),
                    Width = (int)(original.Width / ImageScale),
                    Height = (int)(original.Height / ImageScale)
                };
            }
            return rect;
        }

        private void ButtonDone_Click(object sender, EventArgs e)
        {            
            if (TemplateArea.Area == Rectangle.Empty)
            {
                MessageBox.Show("Template area cannot be empty");
                return;
            }
            var templateRect = ConvertRectangle(TemplateArea.Area);            
            var cropped = OriginalImage.Clone(templateRect, OriginalImage.PixelFormat);

            Rectangle search;
            if (SearchArea.Area == Rectangle.Empty)
            {
                search = new Rectangle(0, 0, 1088, 816);
            }
            else
            {
                search = ConvertRectangle(SearchArea.Area);
            }            
            var click = ConvertRectangle(ClickArea.Area);

            Samples? next = null;
            if (comboBoxNext.SelectedItem != null)
            {
                next = (Samples)comboBoxNext.SelectedItem;
            }
            Template.Add((Samples)comboBoxName.SelectedItem, next, cropped, search, click);

            Close();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            foreach(var button in ButtonList)
            {
                if (clickedButton == button)
                {
                    ActiveArea = (DrawArea)button.Tag;
                    button.Enabled = false;
                }
                else
                {
                    button.Enabled = true;
                }
            }            
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(TemplateArea.Color), TemplateArea.Area);
            e.Graphics.FillRectangle(new SolidBrush(ClickArea.Color), ClickArea.Area);
            e.Graphics.FillRectangle(new SolidBrush(SearchArea.Color), SearchArea.Area);
        }

        public override void Refresh()
        {
            labelSearch.Text = SearchArea.Area.ToString();
            labelClick.Text = ClickArea.Area.ToString();
            labelTemplate.Text = TemplateArea.Area.ToString();
            base.Refresh();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Drawing || e.Button != MouseButtons.Left)
            {
                return;
            }
            ref Rectangle rect = ref ActiveArea.Area;

            rect.X = Origin.X < e.X ? Origin.X : e.X;
            rect.Y = Origin.Y < e.Y ? Origin.Y : e.Y;
            rect.Width = Math.Abs(Origin.X - e.X);
            rect.Height = Math.Abs(Origin.Y - e.Y);

            var lastX = rect.X + rect.Width;
            if (lastX > pictureBox.Image.Width)
            {
                rect.Width = pictureBox.Image.Width - rect.X;
            }
            var lastY = rect.Y + rect.Height;
            if (lastY > pictureBox.Image.Height)
            {
                rect.Height = pictureBox.Image.Height - rect.Y;
            }
            //rect.Y = (int)(rect.Y / ImageScale);
            //rect.X = (int)(rect.X / ImageScale);
            //rect.Width = (int)(rect.Width / ImageScale);
            //rect.Height = (int)(rect.Height / ImageScale);
            Refresh();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            Drawing = false;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            Drawing = true;
            Origin = e.Location;
        }
    }

    class DrawArea
    {
        public Color Color { get; private set; }

        private Rectangle _Area;
        public ref Rectangle Area { get { return ref _Area; } }

        public DrawArea(Color color, Rectangle area)
        {
            Color = color;
            _Area = area;            
        }
    }
}
