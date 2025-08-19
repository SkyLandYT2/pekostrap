using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace PekoStrap
{
    public partial class SettingsForm : Form
    {
        private bool _isDragging = false;
        private Point _startPoint;
        private readonly string _settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pekostrap", "settings.json");

        // Settings class for JSON serialization
        private class AppSettings
        {
            public string GameFolder { get; set; }
            public string LaunchVersion { get; set; }
            public bool EnableFlags { get; set; }
        }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load_1(object sender, EventArgs e)
        {
            DragPanel.MouseDown += DragPanel_MouseDown;
            DragPanel.MouseMove += DragPanel_MouseMove;
            DragPanel.MouseUp += DragPanel_MouseUp_1;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            CloseButton.Click += CloseButton_Click;
            LoadSettings();
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

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.FromArgb(31, 31, 31);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void ShowMessage(string message, bool isError = false)
        {
            SaveInfo.Text = message;
            SaveInfo.ForeColor = isError ? Color.Red : Color.Green;
            SaveInfo.Visible = true;
            await System.Threading.Tasks.Task.Delay(6000);
            SaveInfo.Visible = false;
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                string directory = Path.GetDirectoryName(_settingsPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var settings = new AppSettings
                {
                    GameFolder = SelectGameFolder.SelectedItem?.ToString() ?? "",
                    LaunchVersion = LaunchVersion.SelectedItem?.ToString() ?? "",
                    EnableFlags = EnableFlags.Checked
                };

                string jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingsPath, jsonString);
                ShowMessage("Settings saved successfully!");
            }
            catch (Exception ex)
            {
                ShowMessage($"Error saving settings: {ex.Message}", true);
            }
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(_settingsPath))
                {
                    string jsonString = File.ReadAllText(_settingsPath);
                    var settings = JsonSerializer.Deserialize<AppSettings>(jsonString);

                    if (settings != null)
                    {
                        if (SelectGameFolder.Items.Contains(settings.GameFolder))
                        {
                            SelectGameFolder.SelectedItem = settings.GameFolder;
                        }
                        if (LaunchVersion.Items.Contains(settings.LaunchVersion))
                        {
                            LaunchVersion.SelectedItem = settings.LaunchVersion;
                        }
                        EnableFlags.Checked = settings.EnableFlags;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading settings: {ex.Message}", true);
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            using (MainForm mainForm = new MainForm())
            {
                mainForm.Location = this.Location;
                this.Hide();
                mainForm.FormClosed += (s, args) =>
                {
                    this.Location = mainForm.Location;
                    this.Show();
                };
                mainForm.ShowDialog();
            }
        }

        private void FlagEditor_Click(object sender, EventArgs e)
        {
            using (FlagEditorForm flagEditorForm = new FlagEditorForm())
            {
                flagEditorForm.Location = this.Location;
                this.Hide();
                flagEditorForm.FormClosed += (s, args) =>
                {
                    this.Location = flagEditorForm.Location;
                    this.Show();
                };
                flagEditorForm.ShowDialog();
            }
        }
    }
}