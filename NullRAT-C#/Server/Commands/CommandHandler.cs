using NullRAT.Server.Core;
using NullRAT.Server.UI;
using System;
using System.Threading.Tasks;

namespace NullRAT.Server
{
    public static class CommandHandler
    {
        public static async Task StartCommandLoop()
        {
            while (true)
            {
                ConsoleUI.Prompt();
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;

                string[] parts = input.Split(' ');
                string cmd = parts[0].ToLower();

                switch (cmd)
                {
                    case "agents":
                    case "list":
                        AgentManager.ListAgents();
                        break;

                    case "use":
                        if (parts.Length > 1 && int.TryParse(parts[1], out int id))
                            AgentManager.SwitchAgent(id);
                        else
                            Console.WriteLine("Usage: use <id>");
                        break;

                    case "kill":
                        if (parts.Length > 1 && int.TryParse(parts[1], out int kid))
                            AgentManager.KillAgent(kid);
                        break;

                    case "help":
                        AsciiArt.PrintHelp();
                        break;

                    case "clear":
                        Console.Clear();
                        AsciiArt.PrintBanner();
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        await AgentManager.SendCommand(input);
                        break;
                }
            }
        }
    }
}