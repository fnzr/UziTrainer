namespace UziTrainer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTogglePause = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonLogistics = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.checkBoxSwapActive = new System.Windows.Forms.CheckBox();
            this.textExhausted = new System.Windows.Forms.TextBox();
            this.textLoaded = new System.Windows.Forms.TextBox();
            this.buttonSwap = new System.Windows.Forms.Button();
            this.comboMaps = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSchedule = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textRepeat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textFairyInterval = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonTogglePause
            // 
            this.buttonTogglePause.Location = new System.Drawing.Point(193, 27);
            this.buttonTogglePause.Name = "buttonTogglePause";
            this.buttonTogglePause.Size = new System.Drawing.Size(62, 23);
            this.buttonTogglePause.TabIndex = 0;
            this.buttonTogglePause.Text = "Pause";
            this.buttonTogglePause.UseVisualStyleBackColor = true;
            this.buttonTogglePause.Click += new System.EventHandler(this.buttonTogglePause_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(12, 135);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Run Loop";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonLogistics
            // 
            this.buttonLogistics.Location = new System.Drawing.Point(174, 135);
            this.buttonLogistics.Name = "buttonLogistics";
            this.buttonLogistics.Size = new System.Drawing.Size(75, 23);
            this.buttonLogistics.TabIndex = 2;
            this.buttonLogistics.Text = "Logistics";
            this.buttonLogistics.UseVisualStyleBackColor = true;
            this.buttonLogistics.Click += new System.EventHandler(this.buttonLogistics_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Map";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Doll Out";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Doll In";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(8, 171);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(35, 13);
            this.labelLog.TabIndex = 6;
            this.labelLog.Text = "label4";
            // 
            // checkBoxSwapActive
            // 
            this.checkBoxSwapActive.AutoSize = true;
            this.checkBoxSwapActive.Location = new System.Drawing.Point(12, 62);
            this.checkBoxSwapActive.Name = "checkBoxSwapActive";
            this.checkBoxSwapActive.Size = new System.Drawing.Size(111, 17);
            this.checkBoxSwapActive.TabIndex = 7;
            this.checkBoxSwapActive.Text = "Corpse Dragging?";
            this.checkBoxSwapActive.UseVisualStyleBackColor = true;
            // 
            // textExhausted
            // 
            this.textExhausted.Location = new System.Drawing.Point(50, 83);
            this.textExhausted.Name = "textExhausted";
            this.textExhausted.Size = new System.Drawing.Size(72, 20);
            this.textExhausted.TabIndex = 8;
            // 
            // textLoaded
            // 
            this.textLoaded.Location = new System.Drawing.Point(51, 109);
            this.textLoaded.Name = "textLoaded";
            this.textLoaded.Size = new System.Drawing.Size(71, 20);
            this.textLoaded.TabIndex = 9;
            // 
            // buttonSwap
            // 
            this.buttonSwap.Location = new System.Drawing.Point(117, 58);
            this.buttonSwap.Name = "buttonSwap";
            this.buttonSwap.Size = new System.Drawing.Size(59, 23);
            this.buttonSwap.TabIndex = 10;
            this.buttonSwap.Text = "Swap";
            this.buttonSwap.UseVisualStyleBackColor = true;
            this.buttonSwap.Click += new System.EventHandler(this.buttonSwap_Click);
            // 
            // comboMaps
            // 
            this.comboMaps.FormattingEnabled = true;
            this.comboMaps.Location = new System.Drawing.Point(45, 27);
            this.comboMaps.Name = "comboMaps";
            this.comboMaps.Size = new System.Drawing.Size(57, 21);
            this.comboMaps.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Runs:";
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.Location = new System.Drawing.Point(246, 182);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(13, 13);
            this.labelCounter.TabIndex = 13;
            this.labelCounter.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setConfigMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(267, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setConfigMenuItem
            // 
            this.setConfigMenuItem.Name = "setConfigMenuItem";
            this.setConfigMenuItem.Size = new System.Drawing.Size(55, 20);
            this.setConfigMenuItem.Text = "Config";
            this.setConfigMenuItem.Click += new System.EventHandler(this.setScheduleMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // buttonSchedule
            // 
            this.buttonSchedule.Location = new System.Drawing.Point(93, 135);
            this.buttonSchedule.Name = "buttonSchedule";
            this.buttonSchedule.Size = new System.Drawing.Size(75, 23);
            this.buttonSchedule.TabIndex = 18;
            this.buttonSchedule.Text = "Schedule";
            this.buttonSchedule.UseVisualStyleBackColor = true;
            this.buttonSchedule.Click += new System.EventHandler(this.buttonSchedule_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Repeat";
            // 
            // textRepeat
            // 
            this.textRepeat.Location = new System.Drawing.Point(156, 27);
            this.textRepeat.Name = "textRepeat";
            this.textRepeat.Size = new System.Drawing.Size(20, 20);
            this.textRepeat.TabIndex = 20;
            this.textRepeat.Text = "-1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(128, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Fairy Interval";
            // 
            // textFairyInterval
            // 
            this.textFairyInterval.Location = new System.Drawing.Point(201, 109);
            this.textFairyInterval.Name = "textFairyInterval";
            this.textFairyInterval.Size = new System.Drawing.Size(48, 20);
            this.textFairyInterval.TabIndex = 23;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 204);
            this.Controls.Add(this.textFairyInterval);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textRepeat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonSchedule);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboMaps);
            this.Controls.Add(this.buttonSwap);
            this.Controls.Add(this.textLoaded);
            this.Controls.Add(this.textExhausted);
            this.Controls.Add(this.checkBoxSwapActive);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLogistics);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonTogglePause);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Uzi Trainer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTogglePause;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonLogistics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.CheckBox checkBoxSwapActive;
        private System.Windows.Forms.TextBox textExhausted;
        private System.Windows.Forms.TextBox textLoaded;
        private System.Windows.Forms.Button buttonSwap;
        private System.Windows.Forms.ComboBox comboMaps;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setConfigMenuItem;
        private System.Windows.Forms.Button buttonSchedule;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textRepeat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textFairyInterval;
    }
}

