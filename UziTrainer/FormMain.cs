using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace UziTrainer
{
    public partial class FormMain : Form
    {
        private Thread ExecutionThread;
        private int RunCounter = 0;
        private static readonly string[] maps = {
                "0_2", "1_1N"
        };
        public FormMain()
        {
            InitializeComponent();
            UpdateDollText();
            comboMaps.Items.AddRange(maps);
            checkBoxSwapActive.Checked = Properties.Settings.Default.IsCorpseDragging;
            comboMaps.SelectedIndex = Array.IndexOf(maps, Properties.Settings.Default.SelectedMission);
            comboMaps.SelectedIndexChanged += selectedIndexChanged;

            Trace.Listeners.Add(new FormMainTraceListener(this));

            FormClosing += FormMain_FormClosing;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExecutionThread.Abort();
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SelectedMission = maps[comboMaps.SelectedIndex];
            Properties.Settings.Default.Save();
        }

        private void checkBoxSwapActive_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsCorpseDragging = checkBoxSwapActive.Checked;
            Properties.Settings.Default.Save();
        }

        private void UpdateSettings()
        {
            Properties.Settings.Default.DollExhausted = textExhausted.Text;
            Properties.Settings.Default.DollLoaded = textLoaded.Text;
            Properties.Settings.Default.Save();
            UpdateDollText();
        }

        private void buttonSwap_Click(object sender, EventArgs e)
        {
            var loaded = textExhausted.Text;
            Properties.Settings.Default.DollExhausted = textLoaded.Text;
            Properties.Settings.Default.DollLoaded = loaded;
            Properties.Settings.Default.Save();
            UpdateDollText();
        }

        private void _UpdateDollText()
        {
            textLoaded.Text = Properties.Settings.Default.DollLoaded;
            textExhausted.Text = Properties.Settings.Default.DollExhausted;
        }

        public void UpdateDollText()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)_UpdateDollText);
            }
            else
            {
                _UpdateDollText();
            }
        }

        public void WriteLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => labelLog.Text = message));
            }
            else
            {
                labelLog.Text = message;
            }
        }

        public void PauseExecution()
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => buttonTogglePause.Text = "Resume"));
            }
            else
            {
                buttonTogglePause.Text = "Resume";
            }
            ExecutionThread.Suspend();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            ExecutionThread = new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    Program.Run();
                    BeginInvoke((Action)(() => labelCounter.Text = (++RunCounter).ToString()));
                }
            }));
            ExecutionThread.Start();
        }

        private void buttonTogglePause_Click(object sender, EventArgs e)
        {
            if (ExecutionThread.ThreadState == System.Threading.ThreadState.Suspended)
            {
                buttonTogglePause.Text = "Pause";
                ExecutionThread.Resume();
            }
            else
            {
                buttonTogglePause.Text = "Resume";
                ExecutionThread.Suspend();
            }
        }
    }

    class FormMainTraceListener : TraceListener
    {
        public FormMain form { get; }

        public FormMainTraceListener(FormMain form)
        {
            Name = "FormMainTraceListener";
            this.form = form;
        }

        public override void Write(string message)
        {
            form.WriteLog(message);
        }

        public override void WriteLine(string message)
        {
            form.WriteLog(message);
        }
    }
}
