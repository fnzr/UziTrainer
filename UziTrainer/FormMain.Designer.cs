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
            this.label1 = new System.Windows.Forms.Label();
            this.textExhausted = new System.Windows.Forms.TextBox();
            this.textLoaded = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboMaps = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSwap = new System.Windows.Forms.Button();
            this.checkBoxSwapActive = new System.Windows.Forms.CheckBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonLogistics = new System.Windows.Forms.Button();
            this.labelRunCounter = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.buttonTogglePause = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Exhausted Doll";
            // 
            // textExhausted
            // 
            this.textExhausted.Location = new System.Drawing.Point(107, 68);
            this.textExhausted.Name = "textExhausted";
            this.textExhausted.Size = new System.Drawing.Size(64, 20);
            this.textExhausted.TabIndex = 1;
            // 
            // textLoaded
            // 
            this.textLoaded.Location = new System.Drawing.Point(107, 94);
            this.textLoaded.Name = "textLoaded";
            this.textLoaded.Size = new System.Drawing.Size(64, 20);
            this.textLoaded.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loaded Doll";
            // 
            // comboMaps
            // 
            this.comboMaps.FormattingEnabled = true;
            this.comboMaps.Location = new System.Drawing.Point(47, 12);
            this.comboMaps.Name = "comboMaps";
            this.comboMaps.Size = new System.Drawing.Size(73, 21);
            this.comboMaps.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Map";
            // 
            // buttonSwap
            // 
            this.buttonSwap.Location = new System.Drawing.Point(107, 39);
            this.buttonSwap.Name = "buttonSwap";
            this.buttonSwap.Size = new System.Drawing.Size(64, 23);
            this.buttonSwap.TabIndex = 6;
            this.buttonSwap.Text = "Swap";
            this.buttonSwap.UseVisualStyleBackColor = true;
            this.buttonSwap.Click += new System.EventHandler(this.buttonSwap_Click);
            // 
            // checkBoxSwapActive
            // 
            this.checkBoxSwapActive.AutoSize = true;
            this.checkBoxSwapActive.Location = new System.Drawing.Point(16, 45);
            this.checkBoxSwapActive.Name = "checkBoxSwapActive";
            this.checkBoxSwapActive.Size = new System.Drawing.Size(85, 17);
            this.checkBoxSwapActive.TabIndex = 7;
            this.checkBoxSwapActive.Text = "Swap Dolls?";
            this.checkBoxSwapActive.UseVisualStyleBackColor = true;
            this.checkBoxSwapActive.CheckedChanged += new System.EventHandler(this.checkBoxSwapActive_CheckedChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(197, 12);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 8;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonLogistics
            // 
            this.buttonLogistics.Location = new System.Drawing.Point(197, 38);
            this.buttonLogistics.Name = "buttonLogistics";
            this.buttonLogistics.Size = new System.Drawing.Size(75, 23);
            this.buttonLogistics.TabIndex = 9;
            this.buttonLogistics.Text = "Logistics";
            this.buttonLogistics.UseVisualStyleBackColor = true;
            // 
            // labelRunCounter
            // 
            this.labelRunCounter.AutoSize = true;
            this.labelRunCounter.Location = new System.Drawing.Point(197, 100);
            this.labelRunCounter.Name = "labelRunCounter";
            this.labelRunCounter.Size = new System.Drawing.Size(44, 13);
            this.labelRunCounter.TabIndex = 10;
            this.labelRunCounter.Text = "Runs: 0";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(12, 130);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(54, 13);
            this.labelLog.TabIndex = 11;
            this.labelLog.Text = "Why hello";
            // 
            // buttonTogglePause
            // 
            this.buttonTogglePause.Location = new System.Drawing.Point(219, 120);
            this.buttonTogglePause.Name = "buttonTogglePause";
            this.buttonTogglePause.Size = new System.Drawing.Size(53, 23);
            this.buttonTogglePause.TabIndex = 12;
            this.buttonTogglePause.UseVisualStyleBackColor = true;
            this.buttonTogglePause.Click += new System.EventHandler(this.buttonTogglePause_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 152);
            this.Controls.Add(this.buttonTogglePause);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.labelRunCounter);
            this.Controls.Add(this.buttonLogistics);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.checkBoxSwapActive);
            this.Controls.Add(this.buttonSwap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboMaps);
            this.Controls.Add(this.textLoaded);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textExhausted);
            this.Controls.Add(this.label1);
            this.Name = "FormMain";
            this.Text = "Uzi Trainer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textExhausted;
        private System.Windows.Forms.TextBox textLoaded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboMaps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSwap;
        private System.Windows.Forms.CheckBox checkBoxSwapActive;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonLogistics;
        private System.Windows.Forms.Label labelRunCounter;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Button buttonTogglePause;
    }
}

