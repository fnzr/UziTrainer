using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Forms;

namespace UziTrainer
{

    public partial class FormImage : Form
    {
        Rectangle searchArea = new Rectangle();
        Rectangle clipArea = new Rectangle();
        Point origin;
        string AssetName;
        Bitmap Image;
        MouseButtons button = MouseButtons.None;

        public FormImage(string name, Bitmap image)
        {
            InitializeComponent();
            this.AssetName = name;
            this.Image = image;
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;

            this.Height = image.Height + titleHeight;
            this.Width = image.Width;
            pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseMove += PictureBox1_MouseMove;

            this.KeyUp += FormImage_KeyUp;

        }

        private void FormImage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var template = new Bitmap(clipArea.Width, clipArea.Height);
                using (var g = Graphics.FromImage(template))
                {                    
                    g.DrawImage(Image, 0, 0, clipArea, GraphicsUnit.Pixel);

                    ImageConverter converter = new ImageConverter();
                    var bytes = (byte[])converter.ConvertTo(template, typeof(byte[]));
                    
                    var form = new FormImageInfo(this.AssetName, Convert.ToBase64String(bytes), searchArea);
                    form.ShowDialog();
                }
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Green)), searchArea);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Blue)), clipArea);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            button = e.Button;
            origin = e.Location;
        }

        private void updateRectangle(ref Rectangle rect, Point pos)
        {
            rect.Width = Math.Abs(origin.X - pos.X);
            rect.Height = Math.Abs(origin.Y - pos.Y);

            rect.Y = (pos.Y < origin.Y) ? pos.Y : origin.Y;
            rect.X = (pos.X < origin.X) ? pos.X : origin.X;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (button == MouseButtons.None)
            {
                return;
            }
            if (button == MouseButtons.Left)
            {
                updateRectangle(ref clipArea, e.Location);
            }
            else
            {
                updateRectangle(ref searchArea, e.Location);
            }
            Refresh();
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            button = MouseButtons.None;
        }
    }
}
