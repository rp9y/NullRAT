using NullRAT.Config;
using NullRAT.Server.UI;
using System;

namespace NullRAT.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Config.Load();

            Console.Title = "NullRAT - Server Control";
            AsciiArt.PrintBanner();
            ServerCore.Start();
        }
    }
}