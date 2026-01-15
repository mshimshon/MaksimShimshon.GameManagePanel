using CoreMap;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using MaksimShimshon.GameManagePanel.Core;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Dto;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using StatePulse.Net;
using System.Text.Json;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Hooks.Events.OnBeforeRuntimeInitialization;

[EventBusId(PluginKeys.Events.OnBeforeRuntimeInitialization)]
internal class LoadInstallationStateHandler : IEventBusHandler
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


    public async Task HandleAsync(IEventBusMessage evt)
    {
        string file = _pluginConfiguration.GetConfigFor(LinuxGameServerModule.ModuleName, "installationstate.json");
        if (!File.Exists(file)) return;
        string jsonString = await File.ReadAllTextAsync(file);
        InstallationStateDto result = JsonSerializer.Deserialize<InstallationStateDto>(jsonString)!;
        var entity = _coreMap.Map(result).To<GameServerInfoEntity>();
        await _dispatcher.Prepare<GameServerInstallStateLoadedAction>(entity).DispatchAsync();

    }
}
