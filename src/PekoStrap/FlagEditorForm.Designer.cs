namespace PekoStrap
{
    partial class FlagEditorForm
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
            this.DragPanel = new System.Windows.Forms.Panel();
            this.PekostrapText = new System.Windows.Forms.Label();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.SelectVersion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.ReturnToSettings = new System.Windows.Forms.Button();
            this.RobloxFastFlags = new System.Windows.Forms.Button();
            this.DragPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DragPanel
            // 
            this.DragPanel.Controls.Add(this.PekostrapText);
            this.DragPanel.Controls.Add(this.IconBox);
            this.DragPanel.Location = new System.Drawing.Point(-2, -26);
            this.DragPanel.Name = "DragPanel";
            this.DragPanel.Size = new System.Drawing.Size(599, 53);
            this.DragPanel.TabIndex = 4;
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
            // SelectVersion
            // 
            this.SelectVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SelectVersion.FormattingEnabled = true;
            this.SelectVersion.Items.AddRange(new object[] {
            "2017L",
            "2018L",
            "2020L",
            "2021M"});
            this.SelectVersion.Location = new System.Drawing.Point(162, 37);
            this.SelectVersion.Name = "SelectVersion";
            this.SelectVersion.Size = new System.Drawing.Size(236, 24);
            this.SelectVersion.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(1, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select version";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox1.ForeColor = System.Drawing.Color.Ivory;
            this.richTextBox1.Location = new System.Drawing.Point(6, 69);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(577, 203);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // SaveButton
            // 
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SaveButton.Location = new System.Drawing.Point(404, 37);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(87, 26);
            this.SaveButton.TabIndex = 13;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LoadButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LoadButton.Location = new System.Drawing.Point(496, 37);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(87, 26);
            this.LoadButton.TabIndex = 14;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // ReturnToSettings
            // 
            this.ReturnToSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReturnToSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ReturnToSettings.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ReturnToSettings.Location = new System.Drawing.Point(6, 278);
            this.ReturnToSettings.Name = "ReturnToSettings";
            this.ReturnToSettings.Size = new System.Drawing.Size(87, 26);
            this.ReturnToSettings.TabIndex = 15;
            this.ReturnToSettings.Text = "Close";
            this.ReturnToSettings.UseVisualStyleBackColor = true;
            this.ReturnToSettings.Click += new System.EventHandler(this.ReturnToSettings_Click);
            // 
            // RobloxFastFlags
            // 
            this.RobloxFastFlags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RobloxFastFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.RobloxFastFlags.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.RobloxFastFlags.Location = new System.Drawing.Point(99, 278);
            this.RobloxFastFlags.Name = "RobloxFastFlags";
            this.RobloxFastFlags.Size = new System.Drawing.Size(133, 26);
            this.RobloxFastFlags.TabIndex = 16;
            this.RobloxFastFlags.Text = "Roblox-Fast-Flags";
            this.RobloxFastFlags.UseVisualStyleBackColor = true;
            this.RobloxFastFlags.Click += new System.EventHandler(this.RobloxFastFlags_Click);
            // 
            // FlagEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(595, 313);
            this.Controls.Add(this.RobloxFastFlags);
            this.Controls.Add(this.ReturnToSettings);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectVersion);
            this.Controls.Add(this.DragPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FlagEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FlagEditorForm";
            this.Load += new System.EventHandler(this.FlagEditorForm_Load_1);
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
        private System.Windows.Forms.ComboBox SelectVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button ReturnToSettings;
        private System.Windows.Forms.Button RobloxFastFlags;
    }
}