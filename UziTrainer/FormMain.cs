using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using UziTrainer.Machine;
using UziTrainer.Page;

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
            UpdateDollText();
        }

        private void _UpdateDollText()
        {
            textLoaded.Text = SwapDoll.Default.LoadedDoll;
            textExhausted.Text = SwapDoll.Default.ExhaustedDoll;
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

        public new void Close()
        {
            ExecutionThread.Abort();
            base.Close();
        }

        private void debugMenuItem_Click(object sender, EventArgs e)
        {
            ExecutionThread = new Thread(new ThreadStart(delegate ()
            {
                var machine = new UziMachine();
                machine.ReplaceDoll(Doll.Get("G11"), Doll.Get("M4 SOPMOD II"));
                //machine.formation = new FormationMachine(FormationState.DollSelection);
                //var formation = new FormationMachine(FormationState.DollSelection);
                //formation.Fire(formation.SelectDollTrigger, Doll.Get("G11"));
                //machine.LeaveHome(State.Formation);
                //Console.WriteLine("[{1}] placed and [Status:] {0}", formation, formation.State);
                //Scene scene = new Scene();
                //var repair = new Repair(scene);
                //repair.RepairCritical();
                //Mouse.DragUpToDown(700, 104, 734);
                //Factory.ClickEnhanceableDoll();
                //var factory = new Factory();
                //factory.DollEnhancement();
            }));
            ExecutionThread.Start();
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
