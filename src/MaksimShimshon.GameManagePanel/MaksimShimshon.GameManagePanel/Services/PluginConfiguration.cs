using MaksimShimshon.GameManagePanel.Core;

namespace MaksimShimshon.GameManagePanel.Services;

public record PluginConfiguration
{
    public string PluginFolder { get; }
    public string BashFolder { get; }
    public string ConfigFolder { get; }
    public PluginConfiguration()
    {
        PluginFolder = Path.Combine("/", "usr", "lib", "lunaticpanel", "plugins", BaseInfo.AssemblyName);
        BashFolder = Path.Combine("bash");
        ConfigFolder = Path.Combine("config");
    }

    public string GetConfigBase(string moduleName)
        => Path.Combine(ConfigFolder, moduleName);

    public string GetBashBase(string moduleName)
        => Path.Combine(BashFolder, moduleName);

    public string GetConfigFor(string moduleName, string filename)
    => Path.Combine(GetConfigBase(moduleName), filename);

    public string GetBashFor(string moduleName, string filename)
        => Path.Combine(GetBashBase(moduleName), filename);
}
