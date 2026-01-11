using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;
using MaksimShimshon.GameManagePanel.Services;
using System.Text.Json;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    private readonly LGSMSetupConfiguration _lGSMSetupConfiguration = new();

    public Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default)
        => Task.FromResult(_lGSMSetupConfiguration.AvailableGameServers);

    public LinuxGameServerService(PluginConfiguration pluginConfiguration)
    {
        var configFileName = $"{nameof(LGSMSetupConfiguration).ToLower()}.json";
        var configForInstallPath = Path.Combine(pluginConfiguration.GetConfigBase("linuxgameserver"), configFileName);
        if (File.Exists(configForInstallPath))
        {
            var configForInstallPathJson = File.ReadAllText(configForInstallPath);
            _lGSMSetupConfiguration = JsonSerializer.Deserialize<LGSMSetupConfiguration>(configForInstallPathJson)!;
        }
    }
}
