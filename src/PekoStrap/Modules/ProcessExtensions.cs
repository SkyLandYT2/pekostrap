using System;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace PekoraStrap.Modules
{
    public static class ProcessExtensions
    {
        public static string[] GetCommandLine(this Process process)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        string cmdLine = obj["CommandLine"]?.ToString();
                        if (!string.IsNullOrEmpty(cmdLine))
                        {
                            return cmdLine.Split(' ').Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray();
                        }
                    }
                }
                return Array.Empty<string>();
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
    }
}