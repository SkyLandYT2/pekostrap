using DiscordRPC;
using System;
using System.Collections.Generic;

namespace PekoraStrap.Modules
{
    public class DiscordRpcManager
    {
        private DiscordRpcClient _discordClient;
        private const string CLIENT_ID = "1406048684306600117"; // Ensure this is your Client ID from Discord Developer Portal

        public void UpdateDiscordRPC(string year, string placeName, string imageUrl, string username, string userId, string headshotUrl, DateTime? startTime, string placeId, string launchData, string state = null, string largeImageText = null, string smallImageText = null)
        {
            try
            {
                Console.WriteLine($"[Debug] Updating Discord RPC: Year={year}, PlaceName={placeName}, ImageUrl={imageUrl}, Username={username}, UserId={userId}, HeadshotUrl={headshotUrl}, PlaceId={placeId}, LaunchData={launchData}, State={state}, LargeImageText={largeImageText}, SmallImageText={smallImageText}");

                if (_discordClient == null || _discordClient.IsDisposed)
                {
                    Console.WriteLine($"[Debug] Initializing DiscordRpcClient with Client ID: {CLIENT_ID}");
                    _discordClient = new DiscordRpcClient(CLIENT_ID);
                    try
                    {
                        _discordClient.Initialize();
                        Console.WriteLine("[Debug] DiscordRpcClient initialized successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Debug] Failed to initialize DiscordRpcClient: {ex.Message}");
                        return;
                    }
                }

                var buttons = new List<Button>
                {
                    new Button
                    {
                        Label = "See game page",
                        Url = $"https://www.pekora.zip/games/{placeId}"
                    }
                };

                if (!string.IsNullOrEmpty(launchData))
                {
                    buttons.Add(new Button
                    {
                        Label = "Join server",
                        Url = $"pekora-player:placelauncherurl=https://www.pekora.zip/games/{placeId}?data={launchData}"
                    });
                }

                var presence = new RichPresence
                {
                    Details = placeName.Length > 128 ? placeName.Substring(0, 128) : placeName,
                    State = state != null ? (state.Length > 128 ? state.Substring(0, 128) : state) : $"Pekora ({year})".Length > 128 ? $"Pekora ({year})".Substring(0, 128) : $"Pekora ({year})",
                    Timestamps = new Timestamps(startTime?.ToUniversalTime() ?? DateTime.UtcNow),
                    Assets = new Assets
                    {
                        LargeImageKey = imageUrl ?? "default_large_image",
                        LargeImageText = largeImageText != null ? (largeImageText.Length > 128 ? largeImageText.Substring(0, 128) : largeImageText) : (placeName.Length > 128 ? placeName.Substring(0, 128) : placeName),
                        SmallImageKey = headshotUrl ?? "default_small_image",
                        SmallImageText = smallImageText != null ? (smallImageText.Length > 128 ? smallImageText.Substring(0, 128) : smallImageText) : (userId != null ? $"{username} ({userId})".Length > 128 ? $"{username} ({userId})".Substring(0, 128) : $"{username} ({userId})" : "Unknown User")
                    },
                    Buttons = buttons.ToArray()
                };

                _discordClient.SetPresence(presence);
                Console.WriteLine("[Debug] Discord RPC updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateDiscordRPC] Error: {ex.Message}");
            }
        }

        public void ClearDiscordRPC()
        {
            try
            {
                if (_discordClient != null && !_discordClient.IsDisposed)
                {
                    Console.WriteLine("[Debug] Clearing Discord RPC");
                    _discordClient.ClearPresence();
                    _discordClient.Dispose();
                    _discordClient = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ClearDiscordRPC] Error: {ex.Message}");
            }
        }
    }
}