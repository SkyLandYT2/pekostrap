using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;

namespace PekoStrap
{
    public partial class FlagEditorForm : Form
    {
        private bool _isDragging = false;
        private Point _startPoint;
        private readonly string _fastFlagsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pekostrap", "Fastflags");

        public FlagEditorForm()
        {
            InitializeComponent();
            ConfigureRichTextBox();
        }

        private void FlagEditorForm_Load_1(object sender, EventArgs e)
        {
            DragPanel.MouseDown += DragPanel_MouseDown;
            DragPanel.MouseMove += DragPanel_MouseMove;
            DragPanel.MouseUp += DragPanel_MouseUp_1;
            SaveButton.Click += SaveButton_Click;
            LoadButton.Click += LoadButton_Click;
            richTextBox1.KeyDown += RichTextBox1_KeyDown;
            LoadFlagEditorForm();
        }

        private void LoadFlagEditorForm()
        {
        }

        private void ConfigureRichTextBox()
        {
            richTextBox1.Multiline = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Font = new Font("Consolas", 10);
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                richTextBox1.SelectedText = "\t";
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                int cursorPosition = richTextBox1.SelectionStart;
                string textBeforeCursor = cursorPosition > 0 ? richTextBox1.Text.Substring(0, cursorPosition) : "";
                string currentLine = textBeforeCursor.Split(new[] { "\r\n" }, StringSplitOptions.None).LastOrDefault() ?? "";

                if (currentLine.TrimEnd().EndsWith("{") || currentLine.TrimEnd().EndsWith(","))
                {
                    richTextBox1.SelectedText = "\r\n\t";
                    e.Handled = true;
                }
            }
        }

        private void DragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _startPoint = new Point(e.X, e.Y);
            }
        }

        private void DragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point p = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
            }
        }

        private void DragPanel_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
            }
        }

        private void FlagCloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SelectVersion.Text))
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.Show("Select a version in ComboBox!", SelectVersion, 1000);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    JsonDocument.Parse(richTextBox1.Text);
                    string fileName = $"{SelectVersion.Text}.json";
                    string filePath = Path.Combine(_fastFlagsPath, fileName);

                    Directory.CreateDirectory(_fastFlagsPath);
                    File.WriteAllText(filePath, richTextBox1.Text);

                    ToolTip toolTip = new ToolTip();
                    toolTip.Show($"JSON saved to {fileName}!", richTextBox1, 1000);
                }
                else
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.Show("JSON field is empty!", richTextBox1, 1000);
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Show($"JSON error: {ex.Message}", richTextBox1, 1000);
            }
            catch (Exception ex)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Show($"Save error: {ex.Message}", richTextBox1, 1000);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SelectVersion.Text))
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.Show("Select a version in ComboBox!", SelectVersion, 1000);
                    return;
                }

                string fileName = $"{SelectVersion.Text}.json";
                string filePath = Path.Combine(_fastFlagsPath, fileName);

                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    JsonDocument.Parse(jsonContent);
                    richTextBox1.Text = jsonContent;

                    ToolTip toolTip = new ToolTip();
                    toolTip.Show($"JSON loaded from {fileName}!", richTextBox1, 1000);
                }
                else
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.Show($"File {fileName} not found!", richTextBox1, 1000);
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Show($"JSON error: {ex.Message}", richTextBox1, 1000);
            }
            catch (Exception ex)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Show($"Load error: {ex.Message}", richTextBox1, 1000);
            }
        }

        private void ReturnToSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.Location = this.Location;
                this.Hide();
                settingsForm.FormClosed += (s, args) =>
                {
                    this.Location = settingsForm.Location;
                    this.Show();
                };
                settingsForm.ShowDialog();
            }
        }

        private void RobloxFastFlags_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/Dantezz025/Roblox-Fast-Flags",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening GitHub page: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}