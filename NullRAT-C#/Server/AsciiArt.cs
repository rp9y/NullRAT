using System;

namespace NullRAT.Server.UI
{
    public static class AsciiArt
    {
        public static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
     ███╗   ██╗██╗   ██╗██╗     ██╗         ██████╗  █████╗ ████████╗
     ████╗  ██║██║   ██║██║     ██║         ██╔══██╗██╔══██╗╚══██╔══╝
     ██╔██╗ ██║██║   ██║██║     ██║         ██████╔╝███████║   ██║   
     ██║╚██╗██║██║   ██║██║     ██║         ██╔══██╗██╔══██║   ██║   
     ██║ ╚████║╚██████╔╝███████╗███████╗    ██║  ██║██║  ██║   ██║   
     ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   
                                                                   
                    Multi-Agent Remote Access Tool
");
            Console.ResetColor();
            Console.WriteLine("                  Type 'help' for commands\n");
        }

        public static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
Available Commands:
  agents / list          - Show all connected agents
  use <id>               - Switch to agent by ID
  kill <id>              - Terminate specific agent
  help                   - Show this help
  clear                  - Clear console
  exit                   - Close server
");
            Console.ResetColor();
        }
    }
}
