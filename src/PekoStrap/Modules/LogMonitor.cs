using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using DiscordRPC;
using System.Collections.Generic;

namespace PekoraStrap.Modules
{
    public class LogMonitor
    {
        private readonly DiscordRpcManager _discordRpcManager;
        private readonly string _logDirectory;
        private readonly string _logFilePath;
        private readonly List<string> _logMessages = new List<string>();
        private string _lastLaunchData;
        private string _lastPresenceHash; // Store hash of last applied RichPresence to avoid duplicates

        public LogMonitor(DiscordRpcManager discordRpcManager, string logFilePath)
        {
            _discordRpcManager = discordRpcManager;
            _logFilePath = logFilePath;
            _logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox", "logs");
            _lastLaunchData = null;
            _lastPresenceHash = null;
            Log("LogMonitor initialized");
        }

        private void Log(string message)
        {
            _logMessages.Add($"[{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss.fffZ}] {message}");
        }

        public void SaveLogFile()
        {
            try
            {
                File.AppendAllLines(_logFilePath, _logMessages);
                _logMessages.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving log file: {ex.Message}");
            }
        }

        public async Task<bool> StartMonitoringLogs(string year, string placeId, string placeName, string imageUrl, string username, string userId, string headshotUrl, RichPresence initialPresence, DateTime? processStartTime)
        {
            Log($"Starting log monitoring: year={year}, placeId={placeId}, placeName={placeName}");
            while (true)
            {
                try
                {
                    // Check if either ProjectXPlayerLauncher.exe or ProjectXPlayerBeta.exe is running
                    bool launcherRunning = Process.GetProcessesByName("ProjectXPlayerLauncher").Any();
                    bool playerRunning = Process.GetProcessesByName("ProjectXPlayerBeta").Any();

                    if (!launcherRunning && !playerRunning)
                    {
                        Log("No processes running, clearing RPC and exiting");
                        _discordRpcManager.ClearDiscordRPC();
                        SaveLogFile();
                        return false; // Signal to exit
                    }

                    if (!Directory.Exists(_logDirectory))
                    {
                        Log($"Log directory does not exist: {_logDirectory}");
                        await Task.Delay(5000);
                        continue;
                    }

                    var logFiles = Directory.GetFiles(_logDirectory, "*.log")
                        .Select(f => new { File = f, CreationTime = File.GetCreationTime(f), LastWriteTime = File.GetLastWriteTime(f) })
                        .Where(f => !processStartTime.HasValue || (f.CreationTime >= processStartTime.Value.AddSeconds(-5) || f.LastWriteTime >= processStartTime.Value))
                        .OrderByDescending(f => f.LastWriteTime)
                        .ToList();

                    if (!logFiles.Any())
                    {
                        Log("No valid log files found");
                        await Task.Delay(5000);
                        continue;
                    }

                    string latestLogFile = logFiles.First().File;
                    Log($"Selected log file: {latestLogFile}, CreationTime={logFiles.First().CreationTime}, LastWriteTime={logFiles.First().LastWriteTime}");

                    // Read the log file from bottom up
                    using (var fileStream = new FileStream(latestLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(fileStream))
                    {
                        // Read the entire file content and split into lines
                        string fileContent = await reader.ReadToEndAsync();
                        string[] lines = fileContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        Array.Reverse(lines); // Reverse to start from the bottom (newest)

                        bool processedEntry = false;
                        foreach (var line in lines)
                        {
                            if (line.Contains("[PekostrapRPC]"))
                            {
                                try
                                {
                                    var jsonStartIndex = line.IndexOf("[PekostrapRPC] ") + "[PekostrapRPC] ".Length;
                                    var jsonString = line.Substring(jsonStartIndex);
                                    Log($"Processing [PekostrapRPC] entry: {jsonString}");
                                    var jsonData = JObject.Parse(jsonString);

                                    string command = jsonData["command"]?.ToString();
                                    var data = jsonData["data"];

                                    if (command == "SetRichPresence")
                                    {
                                        string details = data["details"]?.ToString();
                                        string state = data["state"]?.ToString();
                                        string largeImageText = data["largeImage"]?["hoverText"]?.ToString();
                                        string smallImageText = data["smallImage"]?["hoverText"]?.ToString();

                                        // Use initialPresence values unless explicitly overridden
                                        if (details == "<reset>" || string.IsNullOrEmpty(details))
                                        {
                                            details = initialPresence?.Details ?? $"Playing in {placeName}";
                                        }
                                        if (state == "<reset>")
                                        {
                                            state = initialPresence?.State ?? $"Pekora ({year})";
                                        }
                                        else if (string.IsNullOrEmpty(state) && data["state"] != null)
                                        {
                                            state = initialPresence?.State ?? $"Pekora ({year})";
                                        }
                                        else if (data["state"] == null)
                                        {
                                            state = initialPresence?.State ?? $"Pekora ({year})";
                                        }

                                        // Truncate to Discord's 128-character limit
                                        if (!string.IsNullOrEmpty(details) && details.Length > 128)
                                        {
                                            details = details.Substring(0, 128);
                                        }
                                        if (!string.IsNullOrEmpty(state) && state.Length > 128)
                                        {
                                            state = state.Substring(0, 128);
                                        }
                                        if (!string.IsNullOrEmpty(largeImageText) && largeImageText.Length > 128)
                                        {
                                            largeImageText = largeImageText.Substring(0, 128);
                                        }
                                        if (!string.IsNullOrEmpty(smallImageText) && smallImageText.Length > 128)
                                        {
                                            smallImageText = smallImageText.Substring(0, 128);
                                        }

                                        var presence = new RichPresence
                                        {
                                            Details = details,
                                            State = state,
                                            Timestamps = new Timestamps
                                            {
                                                Start = data["timeStart"] != null ? DateTimeOffset.FromUnixTimeSeconds((long)data["timeStart"]).UtcDateTime : (initialPresence?.Timestamps.Start ?? DateTime.UtcNow),
                                                End = data["timeEnd"] != null ? DateTimeOffset.FromUnixTimeSeconds((long)data["timeEnd"]).UtcDateTime : null
                                            },
                                            Assets = new Assets
                                            {
                                                LargeImageKey = data["largeImage"]?["assetId"] != null ? $"https://www.pekora.zip/asset/?id={data["largeImage"]["assetId"]}" : (imageUrl ?? "default_large_image"),
                                                LargeImageText = largeImageText ?? (placeName.Length > 128 ? placeName.Substring(0, 128) : placeName),
                                                SmallImageKey = data["smallImage"]?["assetId"] != null ? $"https://www.pekora.zip/asset/?id={data["smallImage"]["assetId"]}" : (headshotUrl ?? "default_small_image"),
                                                SmallImageText = smallImageText ?? (userId != null ? $"{username} ({userId})".Length > 128 ? $"{username} ({userId})".Substring(0, 128) : $"{username} ({userId})" : "Unknown User")
                                            },
                                            Buttons = new[]
                                            {
                                                new DiscordRPC.Button { Label = "See game page", Url = $"https://www.pekora.zip/games/{placeId}" },
                                                !string.IsNullOrEmpty(_lastLaunchData) ? new DiscordRPC.Button { Label = "Join server", Url = $"pekora-player:placelauncherurl=https://www.pekora.zip/games/{placeId}?data={_lastLaunchData}" } : null
                                            }.Where(b => b != null).ToArray()
                                        };

                                        if (data["largeImage"]?["clear"]?.ToObject<bool>() == true || data["largeImage"]?["reset"]?.ToObject<bool>() == true)
                                        {
                                            presence.Assets.LargeImageKey = imageUrl ?? "default_large_image";
                                            presence.Assets.LargeImageText = placeName;
                                        }

                                        if (data["smallImage"]?["clear"]?.ToObject<bool>() == true || data["smallImage"]?["reset"]?.ToObject<bool>() == true)
                                        {
                                            presence.Assets.SmallImageKey = headshotUrl ?? "default_small_image";
                                            presence.Assets.SmallImageText = userId != null ? $"{username} ({userId})" : "Unknown User";
                                        }

                                        // Create a hash of the relevant RichPresence fields to check for duplicates
                                        string presenceHash = ComputePresenceHash(presence, _lastLaunchData);
                                        Log($"Computed presence hash: {presenceHash}, previous: {_lastPresenceHash}");

                                        // Skip update if the presence is identical to the last applied one
                                        if (presenceHash == _lastPresenceHash)
                                        {
                                            Log("Skipping duplicate RPC update");
                                            break; // No change, skip to next log read cycle
                                        }

                                        // Update Discord RPC and store the new hash
                                        Log($"Updating RPC: Details={presence.Details}, State={presence.State}, LargeImageKey={presence.Assets.LargeImageKey}, SmallImageKey={presence.Assets.SmallImageKey}");
                                        _discordRpcManager.UpdateDiscordRPC(year, presence.Details, presence.Assets.LargeImageKey, username, userId, presence.Assets.SmallImageKey, presence.Timestamps.Start, placeId, _lastLaunchData, presence.State, presence.Assets.LargeImageText, presence.Assets.SmallImageText);
                                        _lastPresenceHash = presenceHash;

                                        processedEntry = true;
                                        break; // Stop processing after the first valid SetRichPresence entry
                                    }
                                    else if (command == "SetLaunchData")
                                    {
                                        _lastLaunchData = data.ToString();
                                        Log($"Processing SetLaunchData: {_lastLaunchData}");
                                        // Only update if launch data has changed
                                        if (_lastLaunchData != _lastPresenceHash)
                                        {
                                            var presence = new RichPresence
                                            {
                                                Details = initialPresence?.Details ?? $"Playing in {placeName}",
                                                State = initialPresence?.State ?? $"Pekora ({year})",
                                                Timestamps = new Timestamps
                                                {
                                                    Start = initialPresence?.Timestamps.Start ?? DateTime.UtcNow,
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
                                                    new DiscordRPC.Button { Label = "See game page", Url = $"https://www.pekora.zip/games/{placeId}" },
                                                    !string.IsNullOrEmpty(_lastLaunchData) ? new DiscordRPC.Button { Label = "Join server", Url = $"pekora-player:placelauncherurl=https://www.pekora.zip/games/{placeId}?data={_lastLaunchData}" } : null
                                                }.Where(b => b != null).ToArray()
                                            };

                                            string presenceHash = ComputePresenceHash(presence, _lastLaunchData);
                                            Log($"Computed presence hash for SetLaunchData: {presenceHash}, previous: {_lastPresenceHash}");
                                            if (presenceHash != _lastPresenceHash)
                                            {
                                                Log($"Updating RPC for SetLaunchData: Details={presence.Details}, State={presence.State}");
                                                _discordRpcManager.UpdateDiscordRPC(year, presence.Details, presence.Assets.LargeImageKey, username, userId, presence.Assets.SmallImageKey, presence.Timestamps.Start, placeId, _lastLaunchData, presence.State, presence.Assets.LargeImageText, presence.Assets.SmallImageText);
                                                _lastPresenceHash = presenceHash;
                                            }
                                            else
                                            {
                                                Log("Skipping duplicate SetLaunchData RPC update");
                                            }
                                        }
                                        processedEntry = true;
                                        break; // Stop processing after the first valid SetLaunchData entry
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log($"Error parsing [PekostrapRPC] JSON: {ex.Message}");
                                }
                            }
                        }

                        // If no valid [PekostrapRPC] entry was found, check processes again
                        if (!processedEntry && !launcherRunning && !playerRunning)
                        {
                            Log("No valid [PekostrapRPC] entries and no processes running, clearing RPC and exiting");
                            _discordRpcManager.ClearDiscordRPC();
                            SaveLogFile();
                            return false; // Signal to exit
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error in StartMonitoringLogs: {ex.Message}");
                }

                await Task.Delay(5000);
            }
        }

        private string ComputePresenceHash(RichPresence presence, string launchData)
        {
            // Combine relevant fields to create a unique hash for comparison
            string combined = $"{presence.Details}|{presence.State}|{presence.Assets.LargeImageKey}|{presence.Assets.LargeImageText}|{presence.Assets.SmallImageKey}|{presence.Assets.SmallImageText}|{presence.Timestamps.Start?.ToString("o")}|{presence.Timestamps.End?.ToString("o")}|{launchData}";
            return combined.GetHashCode().ToString();
        }
    }
}