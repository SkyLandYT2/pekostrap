using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using DiscordRPC;
using PekoraStrap.Modules;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Text.Json;
using System.Linq;

namespace PekoStrap
{
    public partial class MainForm : Form
    {
        private bool _isDragging = false;
        private Point _startPoint;
        private readonly string _settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pekostrap", "settings.json");
        private readonly string _fastflagsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pekostrap", "Fastflags");
        private readonly string _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pekostrap", "Pekostrap.log");
        private readonly DiscordRpcManager _discordRpcManager = new DiscordRpcManager();
        private readonly List<string> _logMessages = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                bool isRegistered = false;
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("pekora-player", false))
                {
                    if (key != null)
                    {
                        using (RegistryKey commandKey = key.OpenSubKey(@"shell\open\command", false))
                        {
                            if (commandKey != null)
                            {
                                string command = commandKey.GetValue("")?.ToString();
                                string expectedCommand = $"\"{Application.ExecutablePath}\" \"%1\"";
                                if (command != null && command.Contains(Application.ExecutablePath))
                                {
                                    isRegistered = true;
                                }
                            }
                        }
                    }
                }

                if (!isRegistered)
                {
                    UriSchemeHandler.RegisterUriScheme();
                    MessageBox.Show("Pekostrap successfully installed in registry", "Installation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering URI scheme: {ex.Message}. Please run the application as an administrator.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DragPanel.MouseDown += DragPanel_MouseDown;
            DragPanel.MouseMove += DragPanel_MouseMove;
            DragPanel.MouseUp += DragPanel_MouseUp;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            CloseButton.Click += CloseButton_Click;
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

        private void DragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
            }
        }

        private void DragPanel_Paint(object sender, PaintEventArgs e)
        {
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
            _discordRpcManager.ClearDiscordRPC();
            Application.Exit();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
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

        private string GetVersionFolder(string version, string gameFolder)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), gameFolder, "Versions");
                if (!Directory.Exists(basePath))
                {
                    Log($"Base path does not exist: {basePath}");
                    return "";
                }

                Dictionary<string, string> versionMap = new Dictionary<string, string>
                {
                    { "2017", "2017L" },
                    { "2018", "2018L" },
                    { "2020", "2020L" },
                    { "2021", "2021M" }
                };

                string versionIdentifier = versionMap.ContainsKey(version?.Replace("L", "").Replace("M", ""))
                    ? versionMap[version.Replace("L", "").Replace("M", "")]
                    : version;

                string versionFolder = Directory.GetDirectories(basePath)
                    .FirstOrDefault(d => d.Contains(versionIdentifier) || Path.GetFileName(d).StartsWith("version-")) ?? "";
                if (string.IsNullOrEmpty(versionFolder))
                {
                    Log($"Version folder not found for version: {version}");
                    return "";
                }

                Log($"Version folder found: {versionFolder}");
                return versionFolder;
            }
            catch (Exception ex)
            {
                Log($"Error in GetVersionFolder: {ex.Message}");
                return "";
            }
        }

        private bool ReadEnableFlags()
        {
            try
            {
                if (!File.Exists(_settingsPath))
                {
                    Log($"Settings file not found: {_settingsPath}, defaulting to EnableFlags=false");
                    return false;
                }

                string jsonContent = File.ReadAllText(_settingsPath);
                using var document = JsonDocument.Parse(jsonContent);
                if (document.RootElement.TryGetProperty("EnableFlags", out JsonElement enableFlagsElement) && enableFlagsElement.ValueKind == JsonValueKind.True)
                {
                    Log($"EnableFlags is true in {_settingsPath}");
                    return true;
                }
                else
                {
                    Log($"EnableFlags is false or not set in {_settingsPath}, defaulting to false");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log($"Error reading settings file {_settingsPath}: {ex.Message}, defaulting to EnableFlags=false");
                return false;
            }
        }

        private void Log(string message)
        {
            _logMessages.Add($"[{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss.fffZ}] {message}");
            try
            {
                File.AppendAllLines(_logFilePath, _logMessages);
                _logMessages.Clear();
            }
            catch (Exception ex)
            {
                // Log error silently to avoid user interruption
            }
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string gameFolder = "ProjectX"; // Default value
                string launchVersion = null;
                bool enableFlags = false;

                if (File.Exists(_settingsPath))
                {
                    try
                    {
                        string jsonContent = File.ReadAllText(_settingsPath);
                        using var document = JsonDocument.Parse(jsonContent);
                        var root = document.RootElement;

                        if (root.TryGetProperty("GameFolder", out JsonElement gameFolderElement) && gameFolderElement.ValueKind == JsonValueKind.String)
                        {
                            gameFolder = gameFolderElement.GetString();
                            if (string.IsNullOrEmpty(gameFolder))
                            {
                                gameFolder = "ProjectX";
                                Log($"GameFolder is empty in {_settingsPath}, defaulting to ProjectX");
                            }
                            else
                            {
                                Log($"GameFolder set to {gameFolder} from {_settingsPath}");
                            }
                        }
                        else
                        {
                            Log($"GameFolder not found in {_settingsPath}, defaulting to ProjectX");
                        }

                        if (root.TryGetProperty("LaunchVersion", out JsonElement launchVersionElement) && launchVersionElement.ValueKind == JsonValueKind.String)
                        {
                            launchVersion = launchVersionElement.GetString();
                            Log($"LaunchVersion set to {launchVersion} from {_settingsPath}");
                        }
                        else
                        {
                            Log($"LaunchVersion not found in {_settingsPath}");
                        }

                        enableFlags = ReadEnableFlags();
                    }
                    catch (Exception ex)
                    {
                        Log($"Error parsing {_settingsPath}: {ex.Message}, defaulting to GameFolder=ProjectX");
                    }
                }
                else
                {
                    Log($"Settings file not found: {_settingsPath}, defaulting to GameFolder=ProjectX");
                }

                if (string.IsNullOrEmpty(launchVersion))
                {
                    MessageBox.Show("Launch version not specified in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string versionFolder = GetVersionFolder(launchVersion, gameFolder);
                if (string.IsNullOrEmpty(versionFolder))
                {
                    MessageBox.Show($"Version folder not found for {launchVersion}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string versionSubFolder = Path.Combine(versionFolder, launchVersion);
                string clientSettingsPath = Path.Combine(versionSubFolder, "ClientSettings");
                string fastFlagFile = Path.Combine(_fastflagsPath, $"{launchVersion}.json");
                string clientAppSettingsPath = Path.Combine(clientSettingsPath, "ClientAppSettings.json");

                try
                {
                    if (!Directory.Exists(versionSubFolder))
                    {
                        Directory.CreateDirectory(versionSubFolder);
                        Log($"Created version subdirectory: {versionSubFolder}");
                    }
                    if (!Directory.Exists(clientSettingsPath))
                    {
                        Directory.CreateDirectory(clientSettingsPath);
                        Log($"Created ClientSettings directory: {clientSettingsPath}");
                    }
                    else
                    {
                        Log($"ClientSettings directory already exists: {clientSettingsPath}");
                    }

                    if (enableFlags && File.Exists(fastFlagFile))
                    {
                        Log($"Found fastflag file: {fastFlagFile}");
                        File.Copy(fastFlagFile, clientAppSettingsPath, true);
                        Log($"Copied {fastFlagFile} to {clientAppSettingsPath}");
                    }
                    else if (!enableFlags)
                    {
                        Log($"Fastflag copying skipped: EnableFlags is set to false in {_settingsPath}");
                    }
                    else
                    {
                        Log($"No fastflag file found: {fastFlagFile}");
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error handling fastflag file: {ex.Message}");
                    MessageBox.Show($"Error handling fastflag file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string exePath = Path.Combine(versionSubFolder, "ProjectXPlayerBeta.exe");
                if (!File.Exists(exePath))
                {
                    MessageBox.Show($"Executable not found at: {exePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = "--app",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process process = Process.Start(startInfo);
                process.EnableRaisingEvents = true;
                process.Exited += (s, args) =>
                {
                    try
                    {
                        if (Directory.Exists(clientSettingsPath))
                        {
                            Directory.Delete(clientSettingsPath, true);
                            Log($"Deleted ClientSettings directory: {clientSettingsPath}");
                        }
                        if (Directory.Exists(versionSubFolder) && !Directory.EnumerateFileSystemEntries(versionSubFolder).Any())
                        {
                            Directory.Delete(versionSubFolder, false);
                            Log($"Deleted empty version subdirectory: {versionSubFolder}");
                        }
                        _discordRpcManager.ClearDiscordRPC();
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        Log($"Error deleting ClientSettings directory: {ex.Message}");
                    }
                };

                Task.Run(() =>
                {
                    try
                    {
                        string year = launchVersion.Replace("L", "").Replace("M", "");
                        string placeId = "0";
                        string placeName = "Unknown Place";
                        string username = "Unknown";
                        string userId = null;
                        string imageUrl = null;
                        string headshotUrl = null;

                        string details = $"Playing from App";
                        string state = $"Pekora ({year})";
                        if (details.Length > 128) details = details.Substring(0, 128);
                        if (state.Length > 128) state = state.Substring(0, 128);

                        RichPresence initialPresence = new RichPresence
                        {
                            Details = details,
                            State = state,
                            Timestamps = new Timestamps
                            {
                                Start = DateTime.UtcNow,
                                End = null
                            },
                            Assets = new Assets
                            {
                                LargeImageKey = imageUrl ?? "default_large_image",
                                LargeImageText = placeName.Length > 128 ? placeName.Substring(0, 128) : placeName,
                                SmallImageKey = headshotUrl ?? "default_small_image",
                                SmallImageText = userId != null ? $"{username} ({userId})".Length > 128 ? $"{username} ({userId})".Substring(0, 128) : $"{username} ({userId})" : "Unknown User"
                            },
                            Buttons = new[]
                            {
                                new DiscordRPC.Button { Label = "See game page", Url = $"https://www.pekora.zip/games/{placeId}" }
                            }
                        };

                        _discordRpcManager.UpdateDiscordRPC(
                            year,
                            initialPresence.Details,
                            initialPresence.Assets.LargeImageKey,
                            username,
                            userId,
                            initialPresence.Assets.SmallImageKey,
                            initialPresence.Timestamps.Start,
                            placeId,
                            null,
                            initialPresence.State,
                            initialPresence.Assets.LargeImageText,
                            initialPresence.Assets.SmallImageText
                        );
                    }
                    catch
                    {
                        // Intentionally empty to fix CS0168 warning
                    }
                });

                this.Hide();
            }
            catch (Exception ex)
            {
                Log($"Error launching application: {ex.Message}");
                MessageBox.Show($"Error launching application: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Uninstallbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Click \"Yes\" if you want to remove Pekostrap from registry", "Uninstall Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    UriSchemeHandler.UnregisterUriScheme();
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during uninstall: {ex.Message}", "Uninstall Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Githubpage_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/SkyLandYT2/pekostrap",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening GitHub page: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pekoraprofile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.pekora.zip/users/6105/profile",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Pekora profile page: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}