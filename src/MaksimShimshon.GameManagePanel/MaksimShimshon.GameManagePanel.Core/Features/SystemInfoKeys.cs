using System.Diagnostics.CodeAnalysis;

namespace MaksimShimshon.GameManagePanel.Core.Features;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class SystemInfoKeys
{
    public static class Events
    {
        /// <summary>
        /// Raised when system resource information changes (CPU, RAM, or disk usage).
        /// </summary>
        public const string OnStateUpdate = $"{BaseInfo.AssemblyName}.{nameof(SystemInfoKeys)}.{nameof(Events)}.{nameof(OnStateUpdate)}";
    }
}
