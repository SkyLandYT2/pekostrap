using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PekoraStrap.Modules;
using DiscordRPC;

namespace PekoraStrap
{
    public class Pekostrap
    {
        private readonly ApiClient _apiClient = new ApiClient();
        private readonly DiscordRpcManager _discordRpcManager = new DiscordRpcManager();
        private readonly LogMonitor _logMonitor;
        private string _lastUri;
        private string _initialToken;
        private string _initialPlaceId;
        private string _initialYear;
        private readonly string _logFilePath = Path.Combine(Path.GetTempPath(), "Pekostrap.log");
        private readonly List<string> _logMessages = new List<string>();
        private const string GAMES_PAGE_URL = "https://www.pekora.zip/games";

        public Pekostrap(string[] args)
        {
            Log("Pekostrap constructor started");
            _logMonitor = new LogMonitor(_discordRpcManager, _logFilePath);

            if (args.Length > 0 && args[0].Equals("--uninstall", StringComparison.OrdinalIgnoreCase))
            {
                Log("Uninstall command received");
                UriSchemeHandler.UnregisterUriScheme();
                SaveLogFile();
                Environment.Exit(0);
                return;
            }

            Log("Registering URI scheme");
            UriSchemeHandler.RegisterUriScheme();

            if (args.Length > 0 && args[0].StartsWith("pekora-player:"))
            {
                _lastUri = args[0];
                Log("Received URI: {URI DELETED}");
                var parameters = ParseUriParameters(_lastUri);
                parameters.TryGetValue("gameinfo", out _initialToken);
                parameters.TryGetValue("clientversion", out _initialYear);
                _initialYear = _initialYear?.Replace("L", "").Replace("M", "");
                parameters.TryGetValue("placelauncherurl", out string placeLauncherUrl);
                _initialPlaceId = ExtractPlaceIdFromUrl(placeLauncherUrl ?? "");
                Log($"Parsed parameters: year={_initialYear}, placeId={_initialPlaceId}, token={{TOKEN DELETED}}");
                Task.Run(() => HandleUriLaunch(args[0])).GetAwaiter().GetResult();
            }
            else
            {
                Log("No valid URI provided, opening games page");
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = GAMES_PAGE_URL,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Log($"Error opening games page: {ex.Message}");
                }
                SaveLogFile();
                Environment.Exit(0);
                return;
            }
        }

        private void Log(string message)
        {
            string sanitizedMessage = message;
            if (!string.IsNullOrEmpty(_initialToken))
            {
                sanitizedMessage = sanitizedMessage.Replace(_initialToken, "{TOKEN DELETED}");
            }
            sanitizedMessage = Regex.Replace(sanitizedMessage, @"-t\s+[^\s]+", "-t {TOKEN DELETED}");
            _logMessages.Add($"[{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss.fffZ}] {sanitizedMessage}");
        }

        private void SaveLogFile()
        {
            try
            {
                File.WriteAllLines(_logFilePath, _logMessages);
                _logMessages.Clear();
            }
            catch (Exception ex)
            {
                Log($"Error saving log file: {ex.Message}");
            }
        }

        private async Task HandleUriLaunch(string uri)
        {
            Log("Handling URI launch");
            try
            {
                var parameters = ParseUriParameters(uri);
                parameters.TryGetValue("clientversion", out string clientVersion);
                parameters.TryGetValue("placelauncherurl", out string placeLauncherUrl);
                string placeId = ExtractPlaceIdFromUrl(placeLauncherUrl ?? "");
                Log($"URI launch parameters: clientVersion={clientVersion}, placeId={placeId}");

                string exePath = GetClientExePath(clientVersion);
                if (File.Exists(exePath))
                {
                    string sanitizedUri = Regex.Replace(uri, @"gameinfo:[^\+]+", "gameinfo:{TOKEN DELETED}");
                    Log($"Starting process: {exePath} with arguments: {sanitizedUri}");
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = uri,
                        UseShellExecute = true
                    });
                }
                else
                {
                    Log($"Executable not found: {exePath}");
                }

                await StartMonitoring();
            }
            catch (Exception ex)
            {
                Log($"Error in HandleUriLaunch: {ex.Message}");
            }
        }

        private Dictionary<string, string> ParseUriParameters(string uri)
        {
            var dict = new Dictionary<string, string>();
            try
            {
                var parts = uri.Substring("pekora-player:".Length).Split('+');
                foreach (var part in parts)
                {
                    var kv = part.Split(new[] { ':' }, 2);
                    if (kv.Length == 2)
                        dict[kv[0]] = kv[0] == "gameinfo" ? "{TOKEN DELETED}" : kv[1];
                }
                Log($"Parsed URI parameters: {string.Join(", ", dict.Select(kv => $"{kv.Key}={kv.Value}"))}");
            }
            catch (Exception ex)
            {
                Log($"Error parsing URI parameters: {ex.Message}");
            }
            return dict;
        }

        private string ExtractPlaceIdFromUrl(string url)
        {
            try
            {
                var query = new Uri(url).Query;
                var placeIdParam = query.Split('&').FirstOrDefault(p => p.StartsWith("placeId="));
                string placeId = placeIdParam?.Split('=')[1] ?? "Unknown";
                Log($"Extracted placeId: {placeId}");
                return placeId;
            }
            catch (Exception ex)
            {
                Log($"Error extracting placeId: {ex.Message}");
                return "Unknown";
            }
        }

        private string GetClientExePath(string version)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProjectX", "Versions");
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

                string[] possibleExeNames = { "ProjectXPlayerBeta.exe", "ProjectXPlayerLauncher.exe", "ProjectXPlayer2017.exe" };
                foreach (var exeName in possibleExeNames)
                {
                    string exePath = Path.Combine(versionFolder, exeName);
                    if (File.Exists(exePath))
                    {
                        Log($"Found executable: {exePath}");
                        return exePath;
                    }
                }

                Log("No executable found in version folder");
                return "";
            }
            catch (Exception ex)
            {
                Log($"Error in GetClientExePath: {ex.Message}");
                return "";
            }
        }

        private async Task StartMonitoring()
        {
            Log("Starting monitoring");
            try
            {
                DateTime startAttempt = DateTime.UtcNow;
                var (processYear, placeId, token, pid, startTime, processName) = GetPekoraYearPlaceAndToken();
                while (processName != "ProjectXPlayerBeta.exe" && processName != "ProjectXPlayer2017.exe" && (DateTime.UtcNow - startAttempt).TotalSeconds < 30)
                {
                    Log($"Waiting for ProjectXPlayerBeta.exe or ProjectXPlayer2017.exe, current process: {processName ?? "none"}");
                    await Task.Delay(1000);
                    (processYear, placeId, token, pid, startTime, processName) = GetPekoraYearPlaceAndToken();
                }

                if (processName != "ProjectXPlayerBeta.exe" && processName != "ProjectXPlayer2017.exe")
                {
                    Log("Timeout: ProjectXPlayerBeta.exe or ProjectXPlayer2017.exe not detected within 30 seconds");
                }

                string year = processYear ?? _initialYear;
                placeId = placeId ?? _initialPlaceId;
                token = token ?? _initialToken;
                Log($"Process info: year={year}, placeId={placeId}, token={{TOKEN DELETED}}, processName={processName}, startTime={startTime?.ToString("o")}");

                string placeName = $"Place ID: {placeId}";
                string imageUrl = null;
                string userId = null;
                string username = "Unknown";
                string headshotUrl = null;

                if (!_apiClient.IsTokenInvalid)
                {
                    Log("Fetching place name and image from API");
                    var placeResult = await _apiClient.GetPlaceNameAndImage(placeId, token);
                    placeName = placeResult.placeName;
                    imageUrl = placeResult.imageUrl;
                    Log($"API place result: placeName={placeName}, imageUrl={imageUrl}");

                    Log("Fetching user info and headshot from API");
                    var userResult = await _apiClient.GetUserInfoAndHeadshot(token);
                    userId = userResult.userId;
                    username = userResult.username ?? "Unknown";
                    headshotUrl = userResult.headshotUrl;
                    Log($"API user result: userId={userId}, username={username}, headshotUrl={headshotUrl}");
                }
                else
                {
                    Log("API token invalid, using default values");
                }

                RichPresence initialPresence = null;
                if (processName == "ProjectXPlayerBeta.exe" || processName == "ProjectXPlayer2017.exe")
                {
                    Log("Setting initial Discord RPC");
                    string details = $"Playing in {placeName}";
                    string state = $"Pekora ({year})";
                    if (details.Length > 128) details = details.Substring(0, 128);
                    if (state.Length > 128) state = state.Substring(0, 128);

                    initialPresence = new RichPresence
                    {
                        Details = details,
                        State = state,
                        Timestamps = new Timestamps
                        {
                            Start = startTime ?? DateTime.UtcNow,
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

                    Log($"Initial RPC: Details={initialPresence.Details}, State={initialPresence.State}, LargeImageKey={initialPresence.Assets.LargeImageKey}, SmallImageKey={initialPresence.Assets.SmallImageKey}");
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
                else
                {
                    Log("Skipping initial RPC: Not ProjectXPlayerBeta.exe or ProjectXPlayer2017.exe");
                }

                Log("Starting log monitoring");
                bool continueRunning = await _logMonitor.StartMonitoringLogs(year, placeId, placeName, imageUrl, username, userId, headshotUrl, initialPresence, startTime);
                if (!continueRunning)
                {
                    Log("Log monitoring stopped, clearing RPC and exiting");
                    _discordRpcManager.ClearDiscordRPC();
                    SaveLogFile();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Log($"Error in StartMonitoring: {ex.Message}");
            }
        }

        private (string year, string placeId, string token, int? pid, DateTime? startTime, string processName) GetPekoraYearPlaceAndToken()
        {
            try
            {
                string[] processNames = { "ProjectXPlayerBeta.exe", "ProjectXPlayerLauncher.exe", "ProjectXPlayer2017.exe" };
                foreach (var processName in processNames)
                {
                    foreach (var proc in Process.GetProcessesByName(processName.Replace(".exe", "")))
                    {
                        try
                        {
                            string exePath = proc.MainModule.FileName;
                            string folder = Path.GetFileName(Path.GetDirectoryName(exePath));
                            string year = null;
                            foreach (string yearLabel in new[] { "2017L", "2018L", "2020L", "2021M" })
                            {
                                if (folder.Contains(yearLabel))
                                {
                                    year = yearLabel.Replace("L", "").Replace("M", "");
                                    break;
                                }
                            }

                            string placeId = null;
                            string token = null;
                            if (processName == "ProjectXPlayerBeta.exe" || processName == "ProjectXPlayer2017.exe")
                            {
                                string[] cmdlineArgs = proc.GetCommandLine();
                                string cmdline = string.Join(" ", cmdlineArgs);

                                var placeMatch = Regex.Match(cmdline, @"placeId=(\d+)");
                                if (placeMatch.Success)
                                    placeId = placeMatch.Groups[1].Value;

                                var tokenMatch = Regex.Match(cmdline, @"-t\s+([^\s]+)");
                                if (tokenMatch.Success)
                                    token = tokenMatch.Groups[1].Value;
                            }

                            Log($"Found process: {processName}, year={year}, placeId={placeId}, token={{TOKEN DELETED}}, pid={proc.Id}, startTime={proc.StartTime}");
                            return (year, placeId, token, proc.Id, proc.StartTime, processName);
                        }
                        catch (Exception ex)
                        {
                            Log($"Error processing process {processName}: {ex.Message}");
                        }
                    }
                }
                Log("No matching process found");
            }
            catch (Exception ex)
            {
                Log($"Error in GetPekoraYearPlaceAndToken: {ex.Message}");
            }
            return (null, null, null, null, null, null);
        }
    }
}