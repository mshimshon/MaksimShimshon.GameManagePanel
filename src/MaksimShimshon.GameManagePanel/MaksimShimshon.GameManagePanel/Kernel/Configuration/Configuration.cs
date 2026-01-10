using MaksimShimshon.GameManagePanel.Core;

namespace MaksimShimshon.GameManagePanel.Kernel.Configuration;

public class Configuration
{
    public GameInfoConfiguration? GameInfo { get; set; }
    public HeartbeatConfiguration? Heartbeat { get; set; }

    public Configuration()
    {
        PluginBaseLocation = Path.Combine("/", "usr", "lib", "lunaticpanel", "plugins", BaseInfo.AssemblyName);
    }
    public string PluginBaseLocation { get; }
}
