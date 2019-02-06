using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using UziTrainer.Page;
using UziTrainer.Scenes;

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
            checkBoxSwapActive.Checked = SwapDoll.Default.Active;
            comboMaps.SelectedIndex = Array.IndexOf(maps, Properties.Settings.Default.SelectedMission);
            comboMaps.SelectedIndexChanged += selectedIndexChanged;
            Trace.Listeners.Add(new FormMainTraceListener(this));
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SelectedMission = maps[comboMaps.SelectedIndex];
            Properties.Settings.Default.Save();
        }

        private void checkBoxSwapActive_CheckedChanged(object sender, EventArgs e)
        {
            SwapDoll.Default.Active = checkBoxSwapActive.Checked;
            SwapDoll.Default.Save();
        }

        private void UpdateSettings()
        {
            SwapDoll.Default.ExhaustedDoll = textExhausted.Text;
            SwapDoll.Default.LoadedDoll = textLoaded.Text;
            SwapDoll.Default.Save();
            UpdateDollText();
        }

        private void buttonSwap_Click(object sender, EventArgs e)
        {
            var loaded = textExhausted.Text;
            SwapDoll.Default.ExhaustedDoll = textLoaded.Text;
            SwapDoll.Default.LoadedDoll = loaded;
            SwapDoll.Default.Save();
        }

        private void UpdateDollText()
        {
            textLoaded.Text = SwapDoll.Default.LoadedDoll;
            textExhausted.Text = SwapDoll.Default.ExhaustedDoll;
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

        public void SetPausedInfo()
        {
            buttonTogglePause.Text = "Resume";
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

        public new void Close()
        {
            ExecutionThread.Abort();
            base.Close();
        }

        private void debugMenuItem_Click(object sender, EventArgs e)
        {
            var executionThread = new Thread(new ThreadStart(delegate ()
            {
                //Mouse.DragUpToDown(700, 104, 734);
                //Factory.ClickEnhanceableDoll();
                Factory.DollEnhancement();
            }));
            executionThread.Start();
        }
    }

    class FormMainTraceListener : TraceListener
    {
        public FormMain form { get; }

        public FormMainTraceListener(FormMain form)
        {
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
