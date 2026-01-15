using LunaticPanel.Core.Abstraction;
using static LunaticPanel.Core.Abstraction.IPluginConfiguration;
namespace MaksimShimshon.GameManagePanel.Kernel.Configuration;

public class PluginConfiguration
{
    public GameInfoConfiguration GameInfo { get; set; } = default!;
    public HeartbeatConfiguration Heartbeat { get; set; } = default!;
    public RepositoryConfiguration Repositories { get; set; } = default!;

    private string UserFolder { get; }
    private string UserPluginFolder { get; }
    private string PluginFolder { get; }
    private string BashFolder { get; }
    private string ReposFolder { get; }
    private string DownloadFolder { get; }
    private string ConfigFolder { get; }
    private string UserConfigFolder { get; }
    private string UserBashFolder { get; }
    public PluginConfiguration(IPluginConfiguration pluginConfiguration)
    {
        PluginFolder = Path.Combine(pluginConfiguration.PluginFolder);
        UserFolder = Path.Combine("/", "home", "lgsm");
        UserPluginFolder = Path.Combine(UserFolder, LunaticPanelFolderName, LunaticPanelPluginsFolderName, pluginConfiguration.LinuxAssemblyName);
        UserConfigFolder = Path.Combine(UserPluginFolder, "config");
        UserBashFolder = Path.Combine(UserPluginFolder, "bash");
        BashFolder = Path.Combine(PluginFolder, "bash");
        ConfigFolder = Path.Combine(PluginFolder, "config");
        ReposFolder = Path.Combine(PluginFolder, "repos");
        DownloadFolder = Path.Combine(UserPluginFolder, "downloads");
    }
    public string GetUserDownloadBase(string moduleName)
        => Path.Combine(DownloadFolder, moduleName.ToLower());
    public string GetReposBase(string moduleName)
        => Path.Combine(ReposFolder, moduleName.ToLower());

    public string GetReposFor(string moduleName, string repos)
        => Path.Combine(ReposFolder, moduleName.ToLower(), repos.ToLower());

    public string GetConfigBase(string moduleName)
    => Path.Combine(ConfigFolder, moduleName.ToLower());

    public string GetBashBase(string moduleName)
        => Path.Combine(BashFolder, moduleName.ToLower());

    public string GetUserConfigBase(string moduleName)
        => Path.Combine(UserConfigFolder, moduleName.ToLower());

    public string GetUserBashBase(string moduleName)
        => Path.Combine(UserBashFolder, moduleName.ToLower());

    public string GetConfigFor(string moduleName, string filename)
        => Path.Combine(GetConfigBase(moduleName), filename);

    public string GetUserConfigFor(string moduleName, string filename)
        => Path.Combine(GetUserConfigBase(moduleName), filename);

    public string GetBashFor(string moduleName, string filename)
        => Path.Combine(GetBashBase(moduleName), filename);
    public string GetBashFor(string moduleName, string filename, params string[] args)
    {
        string inlineParams = string.Join(' ', args);
        return GetBashFor(moduleName, filename + " " + inlineParams);
    }
    public string GetUserBashFor(string moduleName, string filename)
        => Path.Combine(GetUserBashBase(moduleName), filename);
    public string GetUserBashFor(string moduleName, string filename, params string[] args)
    {
        string inlineParams = string.Join(' ', args);
        return GetUserBashFor(moduleName, filename + " " + inlineParams);
    }
}
