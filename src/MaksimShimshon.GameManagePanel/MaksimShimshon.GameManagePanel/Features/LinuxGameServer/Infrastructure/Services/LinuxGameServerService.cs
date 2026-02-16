using CoreMap;
using LunaticPanel.Core.Abstraction.Tools.LinuxCommand;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Dto;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.ConsoleController;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ILinuxCommand _linuxCommand;
    private readonly ILinuxLockFileController _linuxLockFileController;
    private readonly ICrazyReport _crazyReport;
    private readonly ICoreMap _coreMap;

    public LinuxGameServerService(PluginConfiguration pluginConfiguration,
        ILinuxCommand linuxCommand,
        ILinuxLockFileController linuxLockFileController,
        ICrazyReport crazyReport, ICoreMap coreMap)
    {

        _pluginConfiguration = pluginConfiguration;
        _linuxCommand = linuxCommand;
        _linuxLockFileController = linuxLockFileController;
        _crazyReport = crazyReport;
        _coreMap = coreMap;
        crazyReport.SetModule(LinuxGameServerModule.ModuleName);
    }

    public Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default)
    {
        // TODO: Transfer Logic into Bash and only load a file with available list
        // The Bash will two:
        // 1. Get a URL Git -> Download to _pluginConfiguration.GetReposFor(LinuxGameServerModule.ModuleName, "available_games");
        // 2. Build a File from Available Directory follow below';s logic.
        // This service will call upon the built file

        _crazyReport.Report("GetAvailableGames...");
        var reposFolder = _pluginConfiguration.GetReposFor(LinuxGameServerModule.ModuleName, "available_games");
        var gameFolders = Path.Combine(reposFolder, "games");
        _crazyReport.ReportInfo($"Check Games in {gameFolders}");
        var result = new Dictionary<string, string>();
        var targetExist = Directory.Exists(gameFolders);
        _crazyReport.ReportInfo($"Exist? {targetExist}");

        if (targetExist)
        {
            foreach (var dir in Directory.EnumerateDirectories(gameFolders))
            {
                _crazyReport.ReportInfo($"Found {dir}");
                var folderName = Path.GetFileName(dir);
                var jsonPath = Path.Combine(dir, "game_info.json");
                var jsonExist = File.Exists(jsonPath);
                _crazyReport.ReportInfo($"game_info.json Found? {jsonExist}");
                if (!jsonExist) continue;
                var jsonText = File.ReadAllText(jsonPath);
                var json = JsonNode.Parse(jsonText);
                var name = json!["name"]!.GetValue<string>();
                result[folderName] = name;
                _crazyReport.ReportInfo($"{folderName} = {name}");

            }
        }
        return Task.FromResult(result);
    }

    public async Task<GameServerInstallProcessModel?> GetInstallationProgress(CancellationToken cancellation = default)
    {
        string file = _pluginConfiguration.GetConfigFor(LinuxGameServerModule.ModuleName, "installation_progress_state.json");
        if (!File.Exists(file)) return default;
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<InstallationProgressStateDto>(jsonString)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameServerInstallProcessModel>();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }
    }
    public async Task<GameServerInfoEntity?> GetInstalledGameServer(CancellationToken cancellation = default)
    {
        string file = _pluginConfiguration.GetConfigFor(LinuxGameServerModule.ModuleName, "installation_state.json");
        if (!File.Exists(file)) return default;
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            InstallationStateDto result = JsonSerializer.Deserialize<InstallationStateDto>(jsonString)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameServerInfoEntity>();
            return entity;
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            return default;
        }
    }

    public async Task PerformServerInstallation(string gameServer, string displayName, CancellationToken cancellation = default)
    {
        string scriptInstallGameServer = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "install_game_server.sh", gameServer, displayName);
        await _linuxCommand.RunLinuxScript(scriptInstallGameServer);
    }

}
