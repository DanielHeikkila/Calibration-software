namespace test1_472
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.TreeviewDevices = new System.Windows.Forms.TreeView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.OnlineCheckbox1 = new System.Windows.Forms.CheckBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.OpenProjectButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ConsoleAnswer = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.TreeviewDevices);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(15, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 500);
            this.panel1.TabIndex = 0;
            // 
            // TreeviewDevices
            // 
            this.TreeviewDevices.HideSelection = false;
            this.TreeviewDevices.Location = new System.Drawing.Point(365, 21);
            this.TreeviewDevices.Name = "TreeviewDevices";
            this.TreeviewDevices.Size = new System.Drawing.Size(367, 397);
            this.TreeviewDevices.TabIndex = 1;
            this.TreeviewDevices.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeviewDevices_BeforeSelect);
            this.TreeviewDevices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.TreeviewDevices.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeviewDevices_NodeSelect);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.OnlineCheckbox1);
            this.panel4.Controls.Add(this.DeleteButton);
            this.panel4.Controls.Add(this.OpenProjectButton);
            this.panel4.Controls.Add(this.CloseButton);
            this.panel4.Controls.Add(this.SaveButton);
            this.panel4.Location = new System.Drawing.Point(18, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(332, 397);
            this.panel4.TabIndex = 5;
            // 
            // OnlineCheckbox1
            // 
            this.OnlineCheckbox1.AutoSize = true;
            this.OnlineCheckbox1.Location = new System.Drawing.Point(206, 65);
            this.OnlineCheckbox1.Name = "OnlineCheckbox1";
            this.OnlineCheckbox1.Size = new System.Drawing.Size(80, 24);
            this.OnlineCheckbox1.TabIndex = 12;
            this.OnlineCheckbox1.Text = "Online";
            this.OnlineCheckbox1.UseVisualStyleBackColor = true;
            this.OnlineCheckbox1.CheckStateChanged += new System.EventHandler(this.OnlineCheckbox);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(32, 273);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(254, 60);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "Delete project\r\n";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // OpenProjectButton
            // 
            this.OpenProjectButton.Location = new System.Drawing.Point(32, 46);
            this.OpenProjectButton.Name = "OpenProjectButton";
            this.OpenProjectButton.Size = new System.Drawing.Size(168, 60);
            this.OpenProjectButton.TabIndex = 3;
            this.OpenProjectButton.Text = "Open project";
            this.OpenProjectButton.UseVisualStyleBackColor = true;
            this.OpenProjectButton.Click += new System.EventHandler(this.OpenTheOpeningDialog);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(32, 197);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(254, 60);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close TIA project";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(32, 121);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(254, 60);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save project";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ConsoleAnswer);
            this.panel3.Location = new System.Drawing.Point(18, 434);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(714, 44);
            this.panel3.TabIndex = 5;
            // 
            // ConsoleAnswer
            // 
            this.ConsoleAnswer.AutoSize = true;
            this.ConsoleAnswer.Location = new System.Drawing.Point(14, 14);
            this.ConsoleAnswer.Name = "ConsoleAnswer";
            this.ConsoleAnswer.Size = new System.Drawing.Size(780, 20);
            this.ConsoleAnswer.TabIndex = 1;
            this.ConsoleAnswer.Text = "Please remember that going online is the reccomended option. Therefore only proce" +
    "ed offline at your own risk.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(15, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 49);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.SystemColors.InfoText;
            this.Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Close.BackgroundImage")));
            this.Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Close.Location = new System.Drawing.Point(728, 12);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(38, 35);
            this.Close.TabIndex = 2;
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(96, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "TIA Portal project reader";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(781, 584);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ConsoleAnswer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button OpenProjectButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SaveButton;
        public System.Windows.Forms.TreeView TreeviewDevices;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.CheckBox OnlineCheckbox1;
    }
}

