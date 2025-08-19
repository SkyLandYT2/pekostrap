namespace PekoStrap
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DragPanel = new System.Windows.Forms.Panel();
            this.PekostrapText = new System.Windows.Forms.Label();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TextLogo = new System.Windows.Forms.Label();
            this.versiontext = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.LaunchButton = new System.Windows.Forms.Button();
            this.Uninstallbutton = new System.Windows.Forms.Button();
            this.Githubpage = new System.Windows.Forms.Button();
            this.pekoraprofile = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GithubLogo = new System.Windows.Forms.PictureBox();
            this.DragPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GithubLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // DragPanel
            // 
            this.DragPanel.Controls.Add(this.PekostrapText);
            this.DragPanel.Controls.Add(this.IconBox);
            this.DragPanel.Controls.Add(this.CloseButton);
            this.DragPanel.Location = new System.Drawing.Point(-3, -26);
            this.DragPanel.Name = "DragPanel";
            this.DragPanel.Size = new System.Drawing.Size(599, 53);
            this.DragPanel.TabIndex = 2;
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
            // TextLogo
            // 
            this.TextLogo.AutoSize = true;
            this.TextLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextLogo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TextLogo.Location = new System.Drawing.Point(24, 139);
            this.TextLogo.Name = "TextLogo";
            this.TextLogo.Size = new System.Drawing.Size(161, 37);
            this.TextLogo.TabIndex = 3;
            this.TextLogo.Text = "Pekostrap";
            // 
            // versiontext
            // 
            this.versiontext.AutoSize = true;
            this.versiontext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.versiontext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.versiontext.Location = new System.Drawing.Point(28, 176);
            this.versiontext.Name = "versiontext";
            this.versiontext.Size = new System.Drawing.Size(69, 13);
            this.versiontext.TabIndex = 4;
            this.versiontext.Text = "Version 1.0.1";
            // 
            // SettingsButton
            // 
            this.SettingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.SettingsButton.FlatAppearance.BorderSize = 0;
            this.SettingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsButton.ForeColor = System.Drawing.Color.White;
            this.SettingsButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.SettingsButton.Location = new System.Drawing.Point(413, 144);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(147, 37);
            this.SettingsButton.TabIndex = 3;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // LaunchButton
            // 
            this.LaunchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.LaunchButton.FlatAppearance.BorderSize = 0;
            this.LaunchButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LaunchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LaunchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchButton.ForeColor = System.Drawing.Color.White;
            this.LaunchButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.LaunchButton.Location = new System.Drawing.Point(413, 85);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(147, 37);
            this.LaunchButton.TabIndex = 5;
            this.LaunchButton.Text = "Launch Pekora";
            this.LaunchButton.UseVisualStyleBackColor = false;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // Uninstallbutton
            // 
            this.Uninstallbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.Uninstallbutton.FlatAppearance.BorderSize = 0;
            this.Uninstallbutton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Uninstallbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Uninstallbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Uninstallbutton.ForeColor = System.Drawing.Color.White;
            this.Uninstallbutton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Uninstallbutton.Location = new System.Drawing.Point(413, 201);
            this.Uninstallbutton.Name = "Uninstallbutton";
            this.Uninstallbutton.Size = new System.Drawing.Size(147, 37);
            this.Uninstallbutton.TabIndex = 6;
            this.Uninstallbutton.Text = "Uninstall";
            this.Uninstallbutton.UseVisualStyleBackColor = false;
            this.Uninstallbutton.Click += new System.EventHandler(this.Uninstallbutton_Click);
            // 
            // Githubpage
            // 
            this.Githubpage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.Githubpage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Githubpage.FlatAppearance.BorderSize = 0;
            this.Githubpage.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Githubpage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Githubpage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Githubpage.ForeColor = System.Drawing.Color.White;
            this.Githubpage.Location = new System.Drawing.Point(5, 278);
            this.Githubpage.Name = "Githubpage";
            this.Githubpage.Size = new System.Drawing.Size(32, 32);
            this.Githubpage.TabIndex = 7;
            this.Githubpage.UseVisualStyleBackColor = false;
            this.Githubpage.Click += new System.EventHandler(this.Githubpage_Click);
            // 
            // pekoraprofile
            // 
            this.pekoraprofile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.pekoraprofile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pekoraprofile.FlatAppearance.BorderSize = 0;
            this.pekoraprofile.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pekoraprofile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pekoraprofile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pekoraprofile.ForeColor = System.Drawing.Color.White;
            this.pekoraprofile.Location = new System.Drawing.Point(44, 278);
            this.pekoraprofile.Name = "pekoraprofile";
            this.pekoraprofile.Size = new System.Drawing.Size(32, 32);
            this.pekoraprofile.TabIndex = 8;
            this.pekoraprofile.UseVisualStyleBackColor = false;
            this.pekoraprofile.Click += new System.EventHandler(this.pekoraprofile_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::PekoStrap.Properties.Resources.pekora;
            this.pictureBox1.Location = new System.Drawing.Point(44, 278);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // GithubLogo
            // 
            this.GithubLogo.Enabled = false;
            this.GithubLogo.Image = ((System.Drawing.Image)(resources.GetObject("GithubLogo.Image")));
            this.GithubLogo.Location = new System.Drawing.Point(5, 278);
            this.GithubLogo.Name = "GithubLogo";
            this.GithubLogo.Size = new System.Drawing.Size(32, 32);
            this.GithubLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GithubLogo.TabIndex = 3;
            this.GithubLogo.TabStop = false;
            // 
            // MainForm
            // 
            this.AccessibleName = "Pekostrap";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(595, 313);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pekoraprofile);
            this.Controls.Add(this.GithubLogo);
            this.Controls.Add(this.Githubpage);
            this.Controls.Add(this.Uninstallbutton);
            this.Controls.Add(this.LaunchButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.versiontext);
            this.Controls.Add(this.TextLogo);
            this.Controls.Add(this.DragPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pekostrap";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragPanel.ResumeLayout(false);
            this.DragPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GithubLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DragPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.Label PekostrapText;
        private System.Windows.Forms.Label TextLogo;
        private System.Windows.Forms.Label versiontext;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.Button Uninstallbutton;
        private System.Windows.Forms.Button Githubpage;
        private System.Windows.Forms.PictureBox GithubLogo;
        private System.Windows.Forms.Button pekoraprofile;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}