using LunaticPanel.Core.Abstraction.Tools;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.ConsoleController;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using System.Text.Json.Nodes;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ILinuxCommand _linuxCommand;
    private readonly ILinuxLockFileController _linuxLockFileController;
    private readonly ICrazyReport _crazyReport;

    public LinuxGameServerService(PluginConfiguration pluginConfiguration, ILinuxCommand linuxCommand, ILinuxLockFileController linuxLockFileController, ICrazyReport crazyReport)
    {

        _pluginConfiguration = pluginConfiguration;
        _linuxCommand = linuxCommand;
        _linuxLockFileController = linuxLockFileController;
        _crazyReport = crazyReport;
        crazyReport.SetModule(LinuxGameServerModule.ModuleName);
    }

    public Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default)
    {
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

    public async Task<GameServerInfoEntity?> PerformServerInstallation(string gameServer, CancellationToken cancellation = default)
    {
        Guid lockId = Guid.Empty;
        string pathToLockFile = _pluginConfiguration.GetConfigFor(LinuxGameServerModule.ModuleName, ".install_lock");
        if (File.Exists(pathToLockFile))
            throw new WebServiceException("Installation is already in process.");
        try
        {
            lockId = await _linuxLockFileController.TryToLockAsync(pathToLockFile);
            if (lockId == Guid.Empty)
                throw new WebServiceException("Another process is already performing the installation.");
            await File.WriteAllTextAsync(pathToLockFile, lockId.ToString());

            string scriptSetLocalCulture = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "setlocalculture.sh");
            var localeResponse = await _linuxCommand.RunLinuxScriptWithReplyAs<ScriptResponse>(scriptSetLocalCulture);
            if (!localeResponse.Completed)
                throw new WebServiceException(localeResponse.Failure);

            string scriptInstallGameServer = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "installgameserver.sh", gameServer);
            var installGameServer = await _linuxCommand.RunLinuxScriptWithReplyAs<ScriptResponse>(scriptInstallGameServer);
            if (!installGameServer.Completed)
                throw new WebServiceException(localeResponse.Failure);

            return new GameServerInfoEntity()
            {
                Id = gameServer,
                InstallDate = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            throw new WebServiceException(ex.Message);
        }
        finally
        {
            if (lockId != Guid.Empty)
                await _linuxLockFileController.ReleaseLockAsync(lockId);
        }
    }

}
