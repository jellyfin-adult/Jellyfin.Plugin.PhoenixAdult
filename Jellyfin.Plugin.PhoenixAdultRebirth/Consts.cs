using System.Reflection;
using PhoenixAdultRebirth.Helpers;

namespace PhoenixAdultRebirth
{
    public static class Consts
    {
        public const string DatabaseUpdateURL = "https://api.github.com/repos/jellyfin-adult/Jellyfin.Plugin.PhoenixAdultRebirth/contents/data";

#if __EMBY__
        public const string PluginInstance = "Emby.Plugins.PhoenixAdultRebirth";
#else
        public const string SentryDSN = "https://8379d0e7cc2c45d8b1b6928ab8ff84c0@o1140949.ingest.sentry.io/6198587";

        public const string PluginInstance = "Jellyfin.Plugin.PhoenixAdultRebirth";
#endif

        public static readonly string PluginVersion = $"{Plugin.Instance.Version} build {Helper.GetLinkerTime(Assembly.GetAssembly(typeof(Plugin)))}";
    }
}
