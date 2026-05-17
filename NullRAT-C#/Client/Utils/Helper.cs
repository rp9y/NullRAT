using System;
using System.Runtime.InteropServices;

namespace NullRAT.Utils
{
    public static class Helper
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void HideConsole()
        {
            try
            {
                ShowWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, 0);
            }
            catch { }
        }
    }
}