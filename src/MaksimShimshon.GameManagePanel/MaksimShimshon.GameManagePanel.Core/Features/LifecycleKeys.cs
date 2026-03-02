using System.Diagnostics.CodeAnalysis;

namespace MaksimShimshon.GameManagePanel.Core.Features;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class LifecycleKeys
{
    public static class Queries
    {
    }
    public static class Engine { }
    public static class Events
    {
        public static class Scheduled
        {
            public const string GameServerInfoCheck = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(Scheduled)}.{nameof(GameServerInfoCheck)}";

        }
    }


}