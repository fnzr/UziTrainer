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
            this.SuspendLayout();
            // 
            // buttonTogglePause
            // 
            this.buttonTogglePause.Location = new System.Drawing.Point(176, 34);
            this.buttonTogglePause.Name = "buttonTogglePause";
            this.buttonTogglePause.Size = new System.Drawing.Size(75, 23);
            this.buttonTogglePause.TabIndex = 0;
            this.buttonTogglePause.Text = "Pause";
            this.buttonTogglePause.UseVisualStyleBackColor = true;
            this.buttonTogglePause.Click += new System.EventHandler(this.buttonTogglePause_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(257, 34);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonLogistics
            // 
            this.buttonLogistics.Location = new System.Drawing.Point(257, 63);
            this.buttonLogistics.Name = "buttonLogistics";
            this.buttonLogistics.Size = new System.Drawing.Size(75, 23);
            this.buttonLogistics.TabIndex = 2;
            this.buttonLogistics.Text = "Logistics";
            this.buttonLogistics.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Map";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Exhausted Doll";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Loaded Doll";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(12, 156);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(35, 13);
            this.labelLog.TabIndex = 6;
            this.labelLog.Text = "label4";
            // 
            // checkBoxSwapActive
            // 
            this.checkBoxSwapActive.AutoSize = true;
            this.checkBoxSwapActive.Location = new System.Drawing.Point(15, 63);
            this.checkBoxSwapActive.Name = "checkBoxSwapActive";
            this.checkBoxSwapActive.Size = new System.Drawing.Size(111, 17);
            this.checkBoxSwapActive.TabIndex = 7;
            this.checkBoxSwapActive.Text = "Corpse Dragging?";
            this.checkBoxSwapActive.UseVisualStyleBackColor = true;
            // 
            // textExhausted
            // 
            this.textExhausted.Location = new System.Drawing.Point(92, 93);
            this.textExhausted.Name = "textExhausted";
            this.textExhausted.Size = new System.Drawing.Size(100, 20);
            this.textExhausted.TabIndex = 8;
            // 
            // textLoaded
            // 
            this.textLoaded.Location = new System.Drawing.Point(92, 119);
            this.textLoaded.Name = "textLoaded";
            this.textLoaded.Size = new System.Drawing.Size(100, 20);
            this.textLoaded.TabIndex = 9;
            // 
            // buttonSwap
            // 
            this.buttonSwap.Location = new System.Drawing.Point(133, 63);
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
            this.comboMaps.Location = new System.Drawing.Point(49, 31);
            this.comboMaps.Name = "comboMaps";
            this.comboMaps.Size = new System.Drawing.Size(77, 21);
            this.comboMaps.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Runs:";
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.Location = new System.Drawing.Point(286, 119);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(13, 13);
            this.labelCounter.TabIndex = 13;
            this.labelCounter.Text = "0";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 194);
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
            this.Name = "FormMain";
            this.Text = "Form1";
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
    }
}

