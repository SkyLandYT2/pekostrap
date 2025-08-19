using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace PekoraStrap.Modules
{
    public static class UriSchemeHandler
    {
        public static void RegisterUriScheme()
        {
            try
            {
                string exePath = Process.GetCurrentProcess().MainModule.FileName;
                string protocol = "pekora-player";
                using (var key = Registry.ClassesRoot.CreateSubKey(protocol))
                {
                    key.SetValue("", "URL:Pekora Player Protocol");
                    key.SetValue("URL Protocol", "");
                    using (var iconKey = key.CreateSubKey("DefaultIcon"))
                    {
                        iconKey.SetValue("", $"\"{exePath}\",0");
                    }
                    using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                    {
                        commandKey.SetValue("", $"\"{exePath}\" \"%1\"");
                    }
                }
            }
            catch
            {
                // Silently ignore errors
            }
        }

        public static void UnregisterUriScheme()
        {
            try
            {
                string protocol = "pekora-player";
                string username = Environment.UserName;
                string defaultExePath = $"C:\\Users\\{username}\\AppData\\Local\\ProjectX\\Versions\\version-29f22ac5f5de4484\\ProjectXPlayerLauncher.exe";
                using (var key = Registry.ClassesRoot.CreateSubKey(protocol))
                {
                    key.SetValue("", "URL:Pekora Player Protocol");
                    key.SetValue("URL Protocol", "");
                    using (var iconKey = key.CreateSubKey("DefaultIcon"))
                    {
                        iconKey.SetValue("", $"\"{defaultExePath}\",0");
                    }
                    using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                    {
                        commandKey.SetValue("", $"\"{defaultExePath}\" \"%1\"");
                    }
                }
            }
            catch
            {
                // Silently ignore errors
            }
        }
    }
}