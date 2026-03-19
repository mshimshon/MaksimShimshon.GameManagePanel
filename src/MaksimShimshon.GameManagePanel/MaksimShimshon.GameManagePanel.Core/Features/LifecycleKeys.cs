using System.Diagnostics.CodeAnalysis;

namespace MaksimShimshon.GameManagePanel.Core.Features;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class LifecycleKeys
{
    public const string ModuleName = "lifecycle";
    public static class Queries
    {
        /// <summary>
        /// Query key for retrieving raw game information.
        /// </summary>
        /// <remarks>
        /// <para><b>Input:</b></para>
        /// <list type="bullet">
        ///   <item>No Input Required</item>
        /// </list>
        /// <para><b>Reply:</b></para>
        /// <list type="bullet">
        ///   <item>
        ///     Type: string
        ///   </item>
        /// </list>
        /// </remarks>
        public const string GetRawGameInfo = $"{BaseInfo.AssemblyName}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(Queries)}.{nameof(GetRawGameInfo)}";

    }
    public static class Engine { }

    /* TODO: Allow Regular Event to use Scheduled Event Keys
     * Schedule Events are One Id to One Handler because they return reschedule information and cannot have multiple scheduled handlers for it.
     * Schedule Event -> Run Period Basis -> ScheduledEventBus Runs -> Also Run Regular Publish on Event using the Scheduled Key.
     */
    public static class Events
    {
        public static class Scheduled
        {
            /// <summary>
            /// Triggers refresh of the server status. (Scheduled Basis)
            /// </summary>
            /// <remarks>
            /// <para><b>Input:</b></para>
            /// <list type="bullet">
            ///   <item>No Input Required</item>
            /// </list>
            /// </remarks>
            public const string GameServerInfoCheck = $"{BaseInfo.AssemblyName}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(Scheduled)}.{nameof(GameServerInfoCheck)}";

        }
    }


}