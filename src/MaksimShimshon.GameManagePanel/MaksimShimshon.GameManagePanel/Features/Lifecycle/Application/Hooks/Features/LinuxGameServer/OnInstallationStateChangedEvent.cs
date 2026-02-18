using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Hooks.Features.LinuxGameServer.Dto;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Hooks.Features.LinuxGameServer;

[EventBusId(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged)]
internal class OnInstallationStateChangedEvent : IEventBusHandler
{
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<LifecycleGameInfoState> _gameInfoStateAccess;

    public OnInstallationStateChangedEvent(IDispatcher dispatcher, IStateAccessor<LifecycleGameInfoState> gameInfoStateAccess)
    {
        _dispatcher = dispatcher;
        _gameInfoStateAccess = gameInfoStateAccess;
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        if (_gameInfoStateAccess.State.GameInfo != default) return;
        var state = await evt.ReadAs<InstallationStateResponse>();
        if (state.IsInstallationCompleted)
            await _dispatcher.Prepare<LifecycleServerGameInfoUpdateAction>().DispatchAsync();
    }
}
