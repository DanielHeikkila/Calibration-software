namespace StartDriveParameterEditor
{
    partial class ProjectSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSelectionForm));
            this.BtnCancel = new System.Windows.Forms.Button();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.ListviewProjects = new System.Windows.Forms.ListView();
            this.ProjHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnBrowseProjectFolder = new System.Windows.Forms.Button();
            this.TxtBoxProjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtBoxProjectDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(244)))));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.BtnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.BtnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(209)))), ((int)(((byte)(223)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Calibri", 9F);
            this.BtnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnCancel.Location = new System.Drawing.Point(672, 458);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(84, 34);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            // 
            // OpenBtn
            // 
            this.OpenBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(244)))));
            this.OpenBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OpenBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.OpenBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.OpenBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(209)))), ((int)(((byte)(223)))));
            this.OpenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenBtn.Font = new System.Drawing.Font("Calibri", 9F);
            this.OpenBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OpenBtn.Location = new System.Drawing.Point(593, 458);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(73, 34);
            this.OpenBtn.TabIndex = 7;
            this.OpenBtn.Text = "OK";
            this.OpenBtn.UseVisualStyleBackColor = false;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // ListviewProjects
            // 
            this.ListviewProjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProjHeader});
            this.ListviewProjects.HideSelection = false;
            this.ListviewProjects.Location = new System.Drawing.Point(143, 114);
            this.ListviewProjects.Name = "ListviewProjects";
            this.ListviewProjects.Size = new System.Drawing.Size(613, 273);
            this.ListviewProjects.TabIndex = 13;
            this.ListviewProjects.UseCompatibleStateImageBehavior = false;
            this.ListviewProjects.View = System.Windows.Forms.View.Details;
            this.ListviewProjects.SelectedIndexChanged += new System.EventHandler(this.OnselectedProjectChanged);
            this.ListviewProjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListviewProjects_MouseDoubleClick);
            // 
            // ProjHeader
            // 
            this.ProjHeader.Text = "TIA projects in folder";
            this.ProjHeader.Width = 608;
            // 
            // BtnBrowseProjectFolder
            // 
            this.BtnBrowseProjectFolder.Location = new System.Drawing.Point(726, 75);
            this.BtnBrowseProjectFolder.Name = "BtnBrowseProjectFolder";
            this.BtnBrowseProjectFolder.Size = new System.Drawing.Size(30, 25);
            this.BtnBrowseProjectFolder.TabIndex = 12;
            this.BtnBrowseProjectFolder.Text = "...";
            this.BtnBrowseProjectFolder.UseVisualStyleBackColor = true;
            this.BtnBrowseProjectFolder.Click += new System.EventHandler(this.BtnBrowseProjectFolder_Click);
            // 
            // TxtBoxProjectName
            // 
            this.TxtBoxProjectName.BackColor = System.Drawing.Color.White;
            this.TxtBoxProjectName.Location = new System.Drawing.Point(142, 421);
            this.TxtBoxProjectName.Name = "TxtBoxProjectName";
            this.TxtBoxProjectName.ReadOnly = true;
            this.TxtBoxProjectName.Size = new System.Drawing.Size(614, 26);
            this.TxtBoxProjectName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(139, 396);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Selected project:";
            // 
            // TxtBoxProjectDirectory
            // 
            this.TxtBoxProjectDirectory.BackColor = System.Drawing.Color.White;
            this.TxtBoxProjectDirectory.Location = new System.Drawing.Point(142, 77);
            this.TxtBoxProjectDirectory.Name = "TxtBoxProjectDirectory";
            this.TxtBoxProjectDirectory.ReadOnly = true;
            this.TxtBoxProjectDirectory.Size = new System.Drawing.Size(576, 26);
            this.TxtBoxProjectDirectory.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Project folder";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 50);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.InfoText;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(845, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 35);
            this.button2.TabIndex = 15;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ProjectSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(895, 539);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.OpenBtn);
            this.Controls.Add(this.ListviewProjects);
            this.Controls.Add(this.BtnBrowseProjectFolder);
            this.Controls.Add(this.TxtBoxProjectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtBoxProjectDirectory);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ProjectSelectionTitlePanel;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView ListviewProjects;
        private System.Windows.Forms.Button BtnBrowseProjectFolder;
        private System.Windows.Forms.TextBox TxtBoxProjectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtBoxProjectDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblErrorstatusl;
        private System.Windows.Forms.ColumnHeader ProjHeader;
        private System.Windows.Forms.ToolStripStatusLabel LblStatusProjectSize;
        private System.Windows.Forms.ToolStripStatusLabel LblStatusLastModified;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
    }
}