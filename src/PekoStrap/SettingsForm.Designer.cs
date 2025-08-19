namespace PekoStrap
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.DragPanel = new System.Windows.Forms.Panel();
            this.PekostrapText = new System.Windows.Forms.Label();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveInfo = new System.Windows.Forms.Label();
            this.SelectGameFolder = new System.Windows.Forms.ComboBox();
            this.LaunchVersion = new System.Windows.Forms.ComboBox();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.GameFolderText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FlagEditor = new System.Windows.Forms.Button();
            this.EnableFlags = new System.Windows.Forms.CheckBox();
            this.DragPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DragPanel
            // 
            this.DragPanel.Controls.Add(this.PekostrapText);
            this.DragPanel.Controls.Add(this.IconBox);
            this.DragPanel.Controls.Add(this.CloseButton);
            this.DragPanel.Location = new System.Drawing.Point(-2, -26);
            this.DragPanel.Name = "DragPanel";
            this.DragPanel.Size = new System.Drawing.Size(599, 53);
            this.DragPanel.TabIndex = 3;
            // 
            // PekostrapText
            // 
            this.PekostrapText.AutoSize = true;
            this.PekostrapText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PekostrapText.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.PekostrapText.Location = new System.Drawing.Point(31, 33);
            this.PekostrapText.Name = "PekostrapText";
            this.PekostrapText.Size = new System.Drawing.Size(55, 13);
            this.PekostrapText.TabIndex = 2;
            this.PekostrapText.Text = "Pekostrap";
            // 
            // IconBox
            // 
            this.IconBox.Image = global::PekoStrap.Properties.Resources.pekostrap;
            this.IconBox.Location = new System.Drawing.Point(8, 29);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(20, 20);
            this.IconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IconBox.TabIndex = 1;
            this.IconBox.TabStop = false;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.CloseButton.Location = new System.Drawing.Point(569, 24);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 29);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveInfo
            // 
            this.SaveInfo.AutoSize = true;
            this.SaveInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(174)))), ((int)(((byte)(82)))));
            this.SaveInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.SaveInfo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SaveInfo.Location = new System.Drawing.Point(12, 226);
            this.SaveInfo.Name = "SaveInfo";
            this.SaveInfo.Size = new System.Drawing.Size(86, 31);
            this.SaveInfo.TabIndex = 4;
            this.SaveInfo.Text = "label1";
            this.SaveInfo.Visible = false;
            // 
            // SelectGameFolder
            // 
            this.SelectGameFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectGameFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectGameFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SelectGameFolder.FormattingEnabled = true;
            this.SelectGameFolder.Items.AddRange(new object[] {
            "ProjectX",
            "Pekora"});
            this.SelectGameFolder.Location = new System.Drawing.Point(170, 43);
            this.SelectGameFolder.Name = "SelectGameFolder";
            this.SelectGameFolder.Size = new System.Drawing.Size(121, 24);
            this.SelectGameFolder.TabIndex = 5;
            this.SelectGameFolder.Visible = false;
            // 
            // LaunchVersion
            // 
            this.LaunchVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LaunchVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LaunchVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LaunchVersion.FormattingEnabled = true;
            this.LaunchVersion.Items.AddRange(new object[] {
            "2020L",
            "2021M"});
            this.LaunchVersion.Location = new System.Drawing.Point(194, 74);
            this.LaunchVersion.Name = "LaunchVersion";
            this.LaunchVersion.Size = new System.Drawing.Size(121, 24);
            this.LaunchVersion.TabIndex = 6;
            // 
            // ReturnButton
            // 
            this.ReturnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReturnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ReturnButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ReturnButton.Location = new System.Drawing.Point(395, 269);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(87, 34);
            this.ReturnButton.TabIndex = 7;
            this.ReturnButton.Text = "Close";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.SaveSettingsButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SaveSettingsButton.Location = new System.Drawing.Point(497, 269);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(84, 34);
            this.SaveSettingsButton.TabIndex = 8;
            this.SaveSettingsButton.Text = "Save";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // GameFolderText
            // 
            this.GameFolderText.AutoSize = true;
            this.GameFolderText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.GameFolderText.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.GameFolderText.Location = new System.Drawing.Point(13, 41);
            this.GameFolderText.Name = "GameFolderText";
            this.GameFolderText.Size = new System.Drawing.Size(151, 26);
            this.GameFolderText.TabIndex = 9;
            this.GameFolderText.Text = "Game Folder :";
            this.GameFolderText.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(13, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "Launch Version :";
            // 
            // FlagEditor
            // 
            this.FlagEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlagEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.FlagEditor.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FlagEditor.Location = new System.Drawing.Point(12, 269);
            this.FlagEditor.Name = "FlagEditor";
            this.FlagEditor.Size = new System.Drawing.Size(117, 34);
            this.FlagEditor.TabIndex = 11;
            this.FlagEditor.Text = "Flag Editor";
            this.FlagEditor.UseVisualStyleBackColor = true;
            this.FlagEditor.Click += new System.EventHandler(this.FlagEditor_Click);
            // 
            // EnableFlags
            // 
            this.EnableFlags.AutoSize = true;
            this.EnableFlags.Checked = true;
            this.EnableFlags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.EnableFlags.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EnableFlags.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.EnableFlags.Location = new System.Drawing.Point(12, 104);
            this.EnableFlags.Name = "EnableFlags";
            this.EnableFlags.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EnableFlags.Size = new System.Drawing.Size(205, 30);
            this.EnableFlags.TabIndex = 14;
            this.EnableFlags.Text = ": Enable Fastflags";
            this.EnableFlags.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.EnableFlags.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.EnableFlags.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(595, 313);
            this.Controls.Add(this.EnableFlags);
            this.Controls.Add(this.FlagEditor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GameFolderText);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.LaunchVersion);
            this.Controls.Add(this.SelectGameFolder);
            this.Controls.Add(this.SaveInfo);
            this.Controls.Add(this.DragPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pekostrap";
            this.Load += new System.EventHandler(this.SettingsForm_Load_1);
            this.DragPanel.ResumeLayout(false);
            this.DragPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DragPanel;
        private System.Windows.Forms.Label PekostrapText;
        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label SaveInfo;
        private System.Windows.Forms.ComboBox SelectGameFolder;
        private System.Windows.Forms.ComboBox LaunchVersion;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Label GameFolderText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FlagEditor;
        private System.Windows.Forms.CheckBox EnableFlags;
    }
}