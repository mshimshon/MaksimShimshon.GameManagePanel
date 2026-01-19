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
        /// <summary>
        /// Occurs when Game Server Install State Changes
        /// </summary>
        public const string OnGameServerInstallStateChanged = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(OnGameServerInstallStateChanged)}";

        /// <summary>
        /// Raised when a game installation is initiated from the dashboard.
        /// </summary>
        public const string OnGameServerInstall = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(OnGameServerInstall)}";

        /// <summary>
        /// Event name raised when a game server installation completes.
        /// <para>
        /// This event is emitted only while the dashboard is actively running.  
        /// It may not fire if the game server was installed in the background.  
        /// Do not rely on this event as a definitive installation check; 
        /// </para>
        /// <para>
        /// instead,
        /// use <see cref="LinuxGameServerKeys.Queries.IsGameServerInstalled"/>.
        /// </para>
        /// </summary>
        public const string OnGameServerInstalled = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(OnGameServerInstalled)}";
        /// <summary>
        /// Event name raised when a game server installation fails to complete.
        /// <para>
        /// This event is emitted only while the dashboard is actively running.  
        /// It may not fire if the game server installation started in the background.  
        /// Do not rely on this event as a definitive installation check; 
        /// </para>
        /// <para>
        /// instead,
        /// use <see cref="LinuxGameServerKeys.Queries.IsGameServerInstalled"/>.
        /// </para>
        /// </summary>
        public const string OnGameServerInstallFailed = $"{BaseInfo.AssemblyName}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(OnGameServerInstallFailed)}";

    }
}
