using CoreMap;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using MaksimShimshon.GameManagePanel.Core;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Hooks.Events.OnBeforeRuntimeInitialization;

[EventBusId(PluginKeys.Events.OnBeforeRuntimeInitialization)]
internal class LoadInstallationStateHandler : IEventBusHandler
{
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<InstallationState> _installStateAccess;

    public LoadInstallationStateHandler(PluginConfiguration pluginConfiguration,
        ICoreMap coreMap,
        IDispatcher dispatcher,
        IStateAccessor<InstallationState> _installStateAccess)
    {
        _dispatcher = dispatcher;
        this._installStateAccess = _installStateAccess;
    }


    public async Task HandleAsync(IEventBusMessage evt)
    {

        await _dispatcher.Prepare<UpdateInstalledGameServerAction>().DispatchAsync();
        if (_installStateAccess.State.IsInstallationCompleted) return;
        await _dispatcher.Prepare<UpdateProgressStateFromDiskAction>().DispatchAsync();



    }
}
