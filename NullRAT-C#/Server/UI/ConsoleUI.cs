using System;

namespace NullRAT.Server.UI
{
    public static class ConsoleUI
    {
        public static void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
            Console.ResetColor();
        }

        public static void Prompt()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\nNullRAT> ");
            Console.ResetColor();
        }
    }
}