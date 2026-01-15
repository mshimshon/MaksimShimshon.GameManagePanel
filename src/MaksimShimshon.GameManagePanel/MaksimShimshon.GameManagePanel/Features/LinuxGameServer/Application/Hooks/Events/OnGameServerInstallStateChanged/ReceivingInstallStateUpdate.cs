using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Abstraction.StateManagement;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Hooks.Events.OnGameServerInstallStateChanged;

[EventBusId(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged, CrossCircuitReceiver = EventBusSpreadType.CrossCircuitExcludeSender)]
internal class ReceivingInstallStateUpdate : IEventBusHandler
{
    private readonly IStateAccessor<InstallationState> _stateAccessor;
    private readonly IDispatcher _dispatcher;

    public ReceivingInstallStateUpdate(IStateAccessor<InstallationState> stateAccessor, IDispatcher dispatcher)
    {
        _stateAccessor = stateAccessor;
        _dispatcher = dispatcher;
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        var payload = evt.GetData()!.GetDataAs<BusStatePayload>()!;
        long v = evt.GetTick();
        if (_stateAccessor.State.BusVersion >= v) return;
        var nState = payload.GetState<InstallationState>();
        await _dispatcher.Prepare<ReceivingUpdatedInstallStateAction>()
            .With(p => p.NewState, nState)
            .With(p => p.NewTick, v)
            .Await()
            .DispatchAsync();
    }
}
