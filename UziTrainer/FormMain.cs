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
        private static readonly string[] maps = {
                "0_2", "1_1N"
        };
        public FormMain()
        {
            InitializeComponent();
            UpdateDollText();
            comboMaps.Items.AddRange(maps);
            checkBoxSwapActive.Checked = SwapDoll.Default.Active;
            comboMaps.SelectedValue = Properties.Settings.Default.SelectedMission;
            comboMaps.SelectedIndexChanged += selectedIndexChanged;
            textLoaded.LostFocus += TextSwap_TextChanged;
            textExhausted.LostFocus += TextSwap_TextChanged;
            SwapDoll.Default.PropertyChanged += Default_PropertyChanged;
            Trace.Listeners.Add(new FormMainTraceListener(this));
        }

        private void TextSwap_TextChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("LoadedDoll"))
            {
                if (InvokeRequired)
                {
                    Invoke((Action)(() => textLoaded.Text = SwapDoll.Default.LoadedDoll));
                }
                else
                {
                    textExhausted.Text = textLoaded.Text = SwapDoll.Default.LoadedDoll;
                }
            }
            if (e.PropertyName.Equals("ExhaustedDoll"))
            {
                if (InvokeRequired)
                {
                    Invoke((Action)(() => textExhausted.Text = SwapDoll.Default.ExhaustedDoll));
                }
                else
                {
                    textExhausted.Text = SwapDoll.Default.ExhaustedDoll;
                }
            }
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
            SwapDoll.Default.ExhaustedDoll = textLoaded.Text;
            SwapDoll.Default.LoadedDoll = textExhausted.Text;
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
            //buttonTogglePause.Text = "Resume";
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
                //buttonTogglePause.Text = "Pause";
                Program.TrainerThread.Set();
            }
            else
            {
               // buttonTogglePause.Text = "Resume";
                Program.TrainerThread.WaitOne();
            }
        }

        private void buttonDebug_Click(object sender, EventArgs e)
        {
            var executionThread = new Thread(new ThreadStart(delegate ()
            {
                //Formation.SetDragFormation();
                //ImageSearch.FindPoint(new Query("Dolls/G11", true), out _);
                //Combat.Setup("0_2");
                //Mouse.ZoomOut();
                Mouse.DragRightToLeft(300, 1250, 850);
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
