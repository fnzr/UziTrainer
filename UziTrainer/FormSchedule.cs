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
    public partial class FormSchedule : Form
    {
        public FormSchedule()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Schedule != null)
            {
                foreach (string value in Properties.Settings.Default.Schedule)
                {
                    dataGridView.Rows.Add(value);
                }
            }
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
