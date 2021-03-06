﻿using System;
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
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            var location = Program.form.DesktopLocation;
            Location = new Point(location.X + 30, location.Y + 30);
            textNoxTitle.Text = Properties.Settings.Default.WindowTitle;
            textNoxTitle.LostFocus += TextNoxTitle_LostFocus;
            if (Properties.Settings.Default.Schedule != null)
            {
                foreach (string value in Properties.Settings.Default.Schedule)
                {
                    dataGridView.Rows.Add(value);
                }
            }
        }

        private void TextNoxTitle_LostFocus(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowTitle = textNoxTitle.Text;
            Properties.Settings.Default.Save();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var list = new System.Collections.Specialized.StringCollection();
            foreach(DataGridViewRow row in dataGridView.Rows)
            {
                var value = row.Cells["Item"].Value;
                if (value != null)
                {
                    list.Add(value.ToString());
                }
            }
            Properties.Settings.Default.Schedule = list;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
