using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using System.Text.Json;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    private readonly LgsmSetupConfiguration _lGSMSetupConfiguration = new();
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly CommandRunner _commandRunner;

    public LinuxGameServerService(PluginConfiguration pluginConfiguration, CommandRunner commandRunner)
    {
        var configFileName = $"{nameof(LgsmSetupConfiguration).ToLower()}.json";
        var configForInstallPath = Path.Combine(pluginConfiguration.GetConfigBase(LinuxGameServerModule.ModuleName), configFileName);
        if (File.Exists(configForInstallPath))
        {
            var configForInstallPathJson = File.ReadAllText(configForInstallPath);
            _lGSMSetupConfiguration = JsonSerializer.Deserialize<LgsmSetupConfiguration>(configForInstallPathJson)!;
        }

        _pluginConfiguration = pluginConfiguration;
        _commandRunner = commandRunner;
    }
    public Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default)
        => Task.FromResult(_lGSMSetupConfiguration.AvailableGameServers);
    public async Task<GameServerInfoEntity?> PerformServerInstallation(string gameServer, CancellationToken cancellation = default)
    {
        string pathToLockFile = _pluginConfiguration.GetConfigFor(LinuxGameServerModule.ModuleName, ".install_lock");
        if (File.Exists(pathToLockFile))
            throw new WebServiceException("Installation is already in process.");

        try
        {
            string scriptSetLocalCulture = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "setlocalculture.sh");
            var localeResponse = await _commandRunner.RunLinuxScriptWithReplyAs<ScriptResponse>(scriptSetLocalCulture);
            if (!localeResponse.Completed)
                throw new WebServiceException(localeResponse.Failure);

            string scriptInstallGameServer = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "installgameserver.sh", gameServer);
            var installGameServer = await _commandRunner.RunLinuxScriptWithReplyAs<ScriptResponse>(scriptInstallGameServer);
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
            File.Delete(pathToLockFile);
        }
    }

}
