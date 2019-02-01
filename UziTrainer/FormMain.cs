using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace UziTrainer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            SetDolls();
            SetExistingMaps();
            Trace.Listeners.Add(new FormMainTraceListener(this));
        }

        private void checkBoxSwapActive_CheckedChanged(object sender, EventArgs e)
        {
            SwapDoll.Default.Active = checkBoxSwapActive.Checked;
            SwapDoll.Default.Save();
        }

        private void buttonSwap_Click(object sender, EventArgs e)
        {
            SwapDoll.Default.ExhaustedDoll = textLoaded.Text;
            SwapDoll.Default.LoadedDoll = textExhausted.Text;
            SwapDoll.Default.Save();
            SetDolls();
        }

        private void SetDolls()
        {
            textLoaded.Text = SwapDoll.Default.LoadedDoll;
            textExhausted.Text = SwapDoll.Default.ExhaustedDoll;
        }

        private void SetExistingMaps()
        {
            string[] maps =
            {
                "0_2", "1_1N"
            };
            comboMaps.Items.AddRange(maps);
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
            var executionThread = new Thread(new ThreadStart(delegate ()
            {
                Program.Run();
            }));
            executionThread.Start();
        }

        private void buttonTogglePause_Click(object sender, EventArgs e)
        {
            if (Program.TrainerThread.WaitOne(0))
            {
                buttonTogglePause.Text = "Pause";
                Program.TrainerThread.Set();
            }
            else
            {
                buttonTogglePause.Text = "Resume";
                Program.TrainerThread.WaitOne();
            }
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
