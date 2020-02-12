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
    public partial class FormImageInfo : Form
    {
        Rectangle SearchArea;
        string ImageData;
        public FormImageInfo(string imageName, string imageData, Rectangle searchArea)
        {
            InitializeComponent();
            this.textImageName.Text = imageName;
            this.labelSearchArea.Text = searchArea.ToString();            
            this.btnSave.Click += BtnSave_Click;

            this.SearchArea = searchArea;
            this.ImageData = imageData;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var template = new Template(this.SearchArea, this.ImageData);
            Template.Add(this.textImageName.Text, template);
            this.Close();
        }
    }
}
