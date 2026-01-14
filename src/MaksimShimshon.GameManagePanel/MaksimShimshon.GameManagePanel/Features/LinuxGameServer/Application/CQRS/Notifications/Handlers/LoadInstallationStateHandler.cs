using CoreMap;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Dto;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS.Notifications;
using MaksimShimshon.GameManagePanel.Services;
using MedihatR;
using StatePulse.Net;
using System.Text.Json;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Handlers;

internal class LoadInstallationStateHandler : INotificationHandler<BeforeRuntimeInitNotification>
{
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ICoreMap _coreMap;
    private readonly IDispatcher _dispatcher;

    public LoadInstallationStateHandler(PluginConfiguration pluginConfiguration, ICoreMap coreMap, IDispatcher dispatcher)
    {
        _pluginConfiguration = pluginConfiguration;
        _coreMap = coreMap;
        _dispatcher = dispatcher;
    }
    public async Task Handle(BeforeRuntimeInitNotification notification, CancellationToken cancellationToken)
    {
        string file = _pluginConfiguration.GetConfigFor(LinuxGameServerModule.ModuleName, "installationstate.json");
        if (!File.Exists(file)) return;
        string jsonString = await File.ReadAllTextAsync(file);
        InstallationStateDto result = JsonSerializer.Deserialize<InstallationStateDto>(jsonString)!;
        var entity = _coreMap.Map(result).To<GameServerInfoEntity>();
        await _dispatcher.Prepare<GameServerInstallStateLoadedAction>(entity).DispatchAsync();

    }
}
