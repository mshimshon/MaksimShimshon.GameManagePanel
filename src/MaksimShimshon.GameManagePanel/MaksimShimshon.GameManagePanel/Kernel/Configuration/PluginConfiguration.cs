using LunaticPanel.Core.Abstraction;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using static LunaticPanel.Core.Abstraction.IPluginConfiguration;
namespace MaksimShimshon.GameManagePanel.Kernel.Configuration;

public class PluginConfiguration
{
    private readonly ICrazyReport _crazyReport;

    //TODO: CHECK HEARTBEAT SYSTEM REMOVE STATEPULSE LOOP IF ANY
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

    public const string BASH_FOLDER_NAME = "bash";
    public const string CONFIG_FOLDER_NAME = "config";
    public const string LGSM_FOLDER_NAME = "lsgm";
    public const string HOME_FOLDER_NAME = "home";
    public const string REPOS_FOLDER_NAME = "repos";
    public const string DOWNLOAD_FOLDER_NAME = "download";
    public PluginConfiguration(IPluginConfiguration pluginConfiguration, ICrazyReport crazyReport)
    {
        PluginFolder = pluginConfiguration.PluginFolder;
        UserFolder = Path.Combine("/", HOME_FOLDER_NAME, LGSM_FOLDER_NAME);
        UserPluginFolder = Path.Combine(UserFolder, LunaticPanelFolderName, LunaticPanelPluginsFolderName, pluginConfiguration.LinuxAssemblyName);
        UserConfigFolder = Path.Combine(UserPluginFolder, CONFIG_FOLDER_NAME);
        UserBashFolder = Path.Combine(UserPluginFolder, BASH_FOLDER_NAME);
        BashFolder = Path.Combine(PluginFolder, BASH_FOLDER_NAME);
        ConfigFolder = Path.Combine(pluginConfiguration.PluginEtcFolder, CONFIG_FOLDER_NAME);
        ReposFolder = Path.Combine(pluginConfiguration.PluginEtcFolder, REPOS_FOLDER_NAME);
        DownloadFolder = Path.Combine(UserPluginFolder, DOWNLOAD_FOLDER_NAME);
        _crazyReport = crazyReport;
    }


    //TODO: DITCH AND USE IPluginConfiguration.EnsureCreated
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

    public string GetUserDownloadFor(string moduleName, string[] subFolders, string filename)
        => Path.Combine(GetUserDownloadBase(Path.Combine([moduleName, .. subFolders])), filename);

    public string GetUserDownloadFor(string moduleName, string[] subFolders)
    => GetUserDownloadBase(Path.Combine([moduleName, .. subFolders]));

    public string GetReposBase(string moduleName)
        => EnsureCreated(Path.Combine(ReposFolder, moduleName.ToLower()));

    public string GetReposFor(string moduleName, string repos)
        => EnsureCreated(Path.Combine(GetReposBase(moduleName), repos.ToLower()));
    public string GetReposFor(string moduleName, string[] subFolders, string repos)
        => GetReposFor(Path.Combine([moduleName, .. subFolders]), repos);

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

    public string GetUserConfigFor(string moduleName, string[] subFolders, string filename)
    => GetUserConfigFor(Path.Combine([moduleName, .. subFolders]), filename);

    public string GetBashFor(string moduleName, string filename)
        => Path.Combine(GetBashBase(moduleName), filename);

    public string GetBashFor(string moduleName, string filename, params string[] args)
        => GetBashFor(moduleName, filename + " " + _stringifyArguments(args));

    public string GetBashFor(string moduleName, string[] subFolders, string filename, params string[] args)
        => GetBashFor(Path.Combine([moduleName, .. subFolders]), filename, args);

    public string GetUserBashFor(string moduleName, string filename)
        => Path.Combine(GetUserBashBase(moduleName), filename);
    public string GetUserBashFor(string moduleName, string[] subFolders, string filename)
        => Path.Combine(GetUserBashBase(Path.Combine([moduleName, .. subFolders])), filename);

    private Func<string[], string> _stringifyArguments = (args) => string.Join(' ', args.Select(p => $"\\\"{p}\\\""));
    public string GetUserBashFor(string moduleName, string filename, params string[] args)
        => GetUserBashFor(moduleName, filename + " " + _stringifyArguments(args));
    public string GetUserBashFor(string moduleName, string[] subFolders, string filename, params string[] args)
        => GetUserBashFor(Path.Combine([moduleName, .. subFolders]), filename, args);
}
