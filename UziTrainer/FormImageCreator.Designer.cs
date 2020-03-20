namespace UziTrainer
{
    partial class FormImageCreator
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTemplateArea = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonClick = new System.Windows.Forms.Button();
            this.labelTemplate = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelClick = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.comboBoxName = new System.Windows.Forms.ComboBox();
            this.comboBoxNext = new System.Windows.Forms.ComboBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(12, 66);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(335, 209);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.buttonTemplateArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSearch, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonClick, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelTemplate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelSearch, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelClick, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonDone, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxNext, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRefresh, 4, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 60);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonTemplateArea
            // 
            this.buttonTemplateArea.Location = new System.Drawing.Point(3, 3);
            this.buttonTemplateArea.Name = "buttonTemplateArea";
            this.buttonTemplateArea.Size = new System.Drawing.Size(61, 24);
            this.buttonTemplateArea.TabIndex = 0;
            this.buttonTemplateArea.Tag = "";
            this.buttonTemplateArea.Text = "Template";
            this.buttonTemplateArea.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(70, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(61, 24);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Tag = "";
            this.buttonSearch.Text = "Search Area";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // buttonClick
            // 
            this.buttonClick.Location = new System.Drawing.Point(137, 3);
            this.buttonClick.Name = "buttonClick";
            this.buttonClick.Size = new System.Drawing.Size(61, 24);
            this.buttonClick.TabIndex = 2;
            this.buttonClick.Tag = "";
            this.buttonClick.Text = "Click Area";
            this.buttonClick.UseVisualStyleBackColor = true;
            // 
            // labelTemplate
            // 
            this.labelTemplate.AutoSize = true;
            this.labelTemplate.Location = new System.Drawing.Point(3, 30);
            this.labelTemplate.Name = "labelTemplate";
            this.labelTemplate.Size = new System.Drawing.Size(35, 13);
            this.labelTemplate.TabIndex = 3;
            this.labelTemplate.Text = "label1";
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(70, 30);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(35, 13);
            this.labelSearch.TabIndex = 4;
            this.labelSearch.Text = "label2";
            // 
            // labelClick
            // 
            this.labelClick.AutoSize = true;
            this.labelClick.Location = new System.Drawing.Point(137, 30);
            this.labelClick.Name = "labelClick";
            this.labelClick.Size = new System.Drawing.Size(35, 13);
            this.labelClick.TabIndex = 5;
            this.labelClick.Text = "label3";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(271, 3);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(61, 23);
            this.buttonDone.TabIndex = 7;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            // 
            // comboBoxName
            // 
            this.comboBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxName.FormattingEnabled = true;
            this.comboBoxName.Location = new System.Drawing.Point(204, 3);
            this.comboBoxName.Name = "comboBoxName";
            this.comboBoxName.Size = new System.Drawing.Size(61, 21);
            this.comboBoxName.TabIndex = 8;
            // 
            // comboBoxNext
            // 
            this.comboBoxNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNext.FormattingEnabled = true;
            this.comboBoxNext.Location = new System.Drawing.Point(204, 33);
            this.comboBoxNext.Name = "comboBoxNext";
            this.comboBoxNext.Size = new System.Drawing.Size(61, 21);
            this.comboBoxNext.TabIndex = 9;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(271, 33);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(61, 23);
            this.buttonRefresh.TabIndex = 10;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // FormImageCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(359, 277);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox);
            this.Name = "FormImageCreator";
            this.Text = "FormImageCreator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonTemplateArea;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonClick;
        private System.Windows.Forms.Label labelTemplate;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelClick;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.ComboBox comboBoxName;
        private System.Windows.Forms.ComboBox comboBoxNext;
        private System.Windows.Forms.Button buttonRefresh;
    }
}