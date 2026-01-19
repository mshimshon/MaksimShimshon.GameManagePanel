using System.Diagnostics.CodeAnalysis;

namespace MaksimShimshon.GameManagePanel.Core.Features;


[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class HeartbeatKeys
{
    public static class Events
    {
        /// <summary>
        /// Raised on every heartbeat cycle.
        /// <para>
        /// All event handlers are awaited before the next beat is issued.  
        /// Expensive operations will slow down the overall beat frequency.
        /// </para>
        /// </summary>

        public const string OnBeat = $"{BaseInfo.AssemblyName}.{nameof(HeartbeatKeys)}.{nameof(Events)}.{nameof(OnBeat)}";
    }
}