namespace StartDriveParameterEditor
{
    partial class RamToRomDialog
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
            this.ProjectSelectionTitlePanel = new System.Windows.Forms.Panel();
            this.DialogTitleLabel = new System.Windows.Forms.Label();
            this.BtnRamToRomNo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.QuestionLabel = new System.Windows.Forms.Label();
            this.AskAgainCheckbox = new System.Windows.Forms.CheckBox();
            this.ButtonRamToRomOK = new System.Windows.Forms.Button();
            this.ProjectSelectionTitlePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProjectSelectionTitlePanel
            // 
            this.ProjectSelectionTitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(244)))));
            this.ProjectSelectionTitlePanel.Controls.Add(this.DialogTitleLabel);
            this.ProjectSelectionTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProjectSelectionTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.ProjectSelectionTitlePanel.Name = "ProjectSelectionTitlePanel";
            this.ProjectSelectionTitlePanel.Size = new System.Drawing.Size(573, 27);
            this.ProjectSelectionTitlePanel.TabIndex = 8;
            // 
            // DialogTitleLabel
            // 
            this.DialogTitleLabel.AutoSize = true;
            this.DialogTitleLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.DialogTitleLabel.ForeColor = System.Drawing.Color.Black;
            this.DialogTitleLabel.Location = new System.Drawing.Point(2, 3);
            this.DialogTitleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DialogTitleLabel.Name = "DialogTitleLabel";
            this.DialogTitleLabel.Size = new System.Drawing.Size(144, 28);
            this.DialogTitleLabel.TabIndex = 0;
            this.DialogTitleLabel.Text = "RAM TO ROM";
            // 
            // BtnRamToRomNo
            // 
            this.BtnRamToRomNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(244)))));
            this.BtnRamToRomNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.BtnRamToRomNo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.BtnRamToRomNo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.BtnRamToRomNo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(209)))), ((int)(((byte)(223)))));
            this.BtnRamToRomNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRamToRomNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnRamToRomNo.Location = new System.Drawing.Point(357, 125);
            this.BtnRamToRomNo.Name = "BtnRamToRomNo";
            this.BtnRamToRomNo.Size = new System.Drawing.Size(73, 28);
            this.BtnRamToRomNo.TabIndex = 7;
            this.BtnRamToRomNo.Text = "No";
            this.BtnRamToRomNo.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.QuestionLabel);
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 82);
            this.panel1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Do you want to back up the parameters?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "The changed parameters are lost after Power OFF.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 38);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // QuestionLabel
            // 
            this.QuestionLabel.AutoSize = true;
            this.QuestionLabel.Location = new System.Drawing.Point(67, 10);
            this.QuestionLabel.Name = "QuestionLabel";
            this.QuestionLabel.Size = new System.Drawing.Size(435, 20);
            this.QuestionLabel.TabIndex = 3;
            this.QuestionLabel.Text = "The parameters are only saved in the volatile memory (RAM).";
            // 
            // AskAgainCheckbox
            // 
            this.AskAgainCheckbox.AutoSize = true;
            this.AskAgainCheckbox.Location = new System.Drawing.Point(25, 131);
            this.AskAgainCheckbox.Name = "AskAgainCheckbox";
            this.AskAgainCheckbox.Size = new System.Drawing.Size(147, 24);
            this.AskAgainCheckbox.TabIndex = 10;
            this.AskAgainCheckbox.Text = "Don\'t Ask again";
            this.AskAgainCheckbox.UseVisualStyleBackColor = true;
            // 
            // ButtonRamToRomOK
            // 
            this.ButtonRamToRomOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(244)))));
            this.ButtonRamToRomOK.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ButtonRamToRomOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.ButtonRamToRomOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.ButtonRamToRomOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(209)))), ((int)(((byte)(223)))));
            this.ButtonRamToRomOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRamToRomOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonRamToRomOK.Location = new System.Drawing.Point(274, 125);
            this.ButtonRamToRomOK.Name = "ButtonRamToRomOK";
            this.ButtonRamToRomOK.Size = new System.Drawing.Size(73, 28);
            this.ButtonRamToRomOK.TabIndex = 6;
            this.ButtonRamToRomOK.Text = "Yes";
            this.ButtonRamToRomOK.UseVisualStyleBackColor = false;
            // 
            // RamToRomDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 190);
            this.Controls.Add(this.ProjectSelectionTitlePanel);
            this.Controls.Add(this.BtnRamToRomNo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AskAgainCheckbox);
            this.Controls.Add(this.ButtonRamToRomOK);
            this.Name = "RamToRomDialog";
            this.Text = "Form3";
            this.ProjectSelectionTitlePanel.ResumeLayout(false);
            this.ProjectSelectionTitlePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ProjectSelectionTitlePanel;
        private System.Windows.Forms.Label DialogTitleLabel;
        private System.Windows.Forms.Button BtnRamToRomNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label QuestionLabel;
        private System.Windows.Forms.CheckBox AskAgainCheckbox;
        private System.Windows.Forms.Button ButtonRamToRomOK;
    }
}