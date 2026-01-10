using System.Diagnostics.CodeAnalysis;

namespace MaksimShimshon.GameManagePanel.Core.Features;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class LinuxGameServerKeys
{
    public static class Queries
    {
        public const string IsGameServerInstalled = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Queries)}.{nameof(IsGameServerInstalled)}";
        public const string GetServerInstallState = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Queries)}.{nameof(GetServerInstallState)}";
    }
    public static class Engine { }
    public static class Events
    {

    }
}
