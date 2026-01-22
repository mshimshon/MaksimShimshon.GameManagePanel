using LunaticPanel.Core.Abstraction;
using MaksimShimshon.GameManagePanel.Kernel.ConsoleController;
using static LunaticPanel.Core.Abstraction.IPluginConfiguration;
namespace MaksimShimshon.GameManagePanel.Kernel.Configuration;

public class PluginConfiguration
{
    private readonly ICrazyReport _crazyReport;

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
    public PluginConfiguration(IPluginConfiguration pluginConfiguration, ICrazyReport crazyReport)
    {
        PluginFolder = pluginConfiguration.PluginFolder;
        UserFolder = Path.Combine("/", "home", "lgsm");
        UserPluginFolder = Path.Combine(UserFolder, LunaticPanelFolderName, LunaticPanelPluginsFolderName, pluginConfiguration.LinuxAssemblyName);
        UserConfigFolder = Path.Combine(UserPluginFolder, "config");
        UserBashFolder = Path.Combine(UserPluginFolder, "bash");
        BashFolder = Path.Combine(PluginFolder, "bash");
        ConfigFolder = Path.Combine(pluginConfiguration.PluginEtcFolder, "config");
        ReposFolder = Path.Combine(pluginConfiguration.PluginEtcFolder, "repos");
        DownloadFolder = Path.Combine(UserPluginFolder, "downloads");
        _crazyReport = crazyReport;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public string EnsureCreated(string path)
    {
        var dir = File.Exists(path) ? Path.GetDirectoryName(path) : path;
        bool doesNotExist = !Directory.Exists(dir);
        //_crazyReport.ReportInfo("Ensure Path {0} is Created ({1})", dir, !doesNotExist);
        if (doesNotExist)
        {
            _crazyReport.ReportInfo($"Created (755): {dir}");
            Directory.CreateDirectory(dir!,
                UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute |
                UnixFileMode.GroupRead | UnixFileMode.GroupExecute |
                UnixFileMode.OtherRead | UnixFileMode.OtherExecute);
        }
        return path;
    }
    public string GetUserDownloadBase(string moduleName)
        => EnsureCreated(Path.Combine(DownloadFolder, moduleName.ToLower()));
    public string GetReposBase(string moduleName)
        => EnsureCreated(Path.Combine(ReposFolder, moduleName.ToLower()));

    public string GetReposFor(string moduleName, string repos)
        => EnsureCreated(Path.Combine(GetReposBase(moduleName), repos.ToLower()));

    public string GetConfigBase(string moduleName)
    => EnsureCreated(Path.Combine(ConfigFolder, moduleName.ToLower()));

    public string GetBashBase(string moduleName)
        => EnsureCreated(Path.Combine(BashFolder, moduleName.ToLower()));

    public string GetUserConfigBase(string moduleName)
        => EnsureCreated(Path.Combine(UserConfigFolder, moduleName.ToLower()));

    public string GetUserBashBase(string moduleName)
        => EnsureCreated(Path.Combine(UserBashFolder, moduleName.ToLower()));

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
