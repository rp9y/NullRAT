using NullRAT.Config; // Shared config

namespace NullRAT.Client
{
    public static class ClientConfig
    {
        public static string CurrentID => Config.VictimID;
    }
}