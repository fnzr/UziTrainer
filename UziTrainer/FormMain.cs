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
        private static readonly string[] Maps = {
                "0_2", "1_6", "1_1N", "2_6", "3_6", "4_6", "5_6", "6_3N", "6_6",
        };
        public FormMain()
        {
            InitializeComponent();
            UpdateDollText();
            comboMaps.Items.AddRange(Maps);
            checkBoxSwapActive.Checked = Properties.Settings.Default.IsCorpseDragging;
            checkBoxSwapActive.CheckedChanged += CheckBoxSwapActive_CheckedChanged;
            comboMaps.SelectedIndex = Array.IndexOf(Maps, Properties.Settings.Default.SelectedMission);
            comboMaps.SelectedIndexChanged += selectedIndexChanged;            
            Trace.Listeners.Add(new FormMainTraceListener(this));

            FormClosing += FormMain_FormClosing;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            TerminateExecutionThread();
        }

        void TerminateExecutionThread()
        {
            if (ExecutionThread != null) // deal with suspended thread
            {
                try
                {
                    ExecutionThread.Abort();
                }
                catch (ThreadStateException)
                {
                    ExecutionThread.Resume();
                }
            }
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SelectedMission = Maps[comboMaps.SelectedIndex];
            if (Maps[comboMaps.SelectedIndex] == "6_3N")
            {
                checkBoxSwapActive.Checked = false;
            }
            Properties.Settings.Default.Save();
        }

        private void CheckBoxSwapActive_CheckedChanged(object sender, EventArgs e)
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

        public void IncreaseCounter()
        {
            BeginInvoke((Action)(() => labelCounter.Text = (++RunCounter).ToString()));
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            var map = Maps[comboMaps.SelectedIndex];
            ExecutionThread = new Thread(new ThreadStart(delegate ()
            {
                Program.Run(map, -1);
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

        private void setScheduleMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormConfig();
            form.Show();
        }

        private void buttonSchedule_Click(object sender, EventArgs e)
        {
            checkBoxSwapActive.Checked = false;
            TerminateExecutionThread();
            ExecutionThread = new Thread(new ThreadStart(delegate ()
            {
                foreach(string mission in Properties.Settings.Default.Schedule)
                {
                    Program.Run(mission, 1);
                }
            }));
            ExecutionThread.Start();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminateExecutionThread();
            ExecutionThread = new Thread(new ThreadStart(delegate ()
            {
                foreach (string mission in Properties.Settings.Default.Schedule)
                {
                    Program.RunTest();
                }
            }));
            ExecutionThread.Start();
        }

        private void buttonLogistics_Click(object sender, EventArgs e)
        {
            TerminateExecutionThread();
            ExecutionThread = new Thread(new ThreadStart(delegate ()
            {
                foreach (string mission in Properties.Settings.Default.Schedule)
                {
                    Program.LogisticsCheck();
                }
            }));
            ExecutionThread.Start();
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
