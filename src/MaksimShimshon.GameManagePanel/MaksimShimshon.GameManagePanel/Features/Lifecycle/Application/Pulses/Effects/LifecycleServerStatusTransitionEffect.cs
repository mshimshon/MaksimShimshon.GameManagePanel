using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class LifecycleServerStatusTransitionEffect : IEffect<LifecycleServerStatusUpdateDoneAction>
{
    private readonly IStateAccessor<LifecycleServerState> _stateAccessor;

    public LifecycleServerStatusTransitionEffect(IStateAccessor<LifecycleServerState> stateAccessor)
    {
        _stateAccessor = stateAccessor;
    }
    public async Task EffectAsync(LifecycleServerStatusUpdateDoneAction action, IDispatcher dispatcher)
    {
        if (_stateAccessor.State.Transition == ServerTransition.Idle ||
            _stateAccessor.State.ServerInfo == default)
            return;

        // Cancel
        if (_stateAccessor.State.TransitionTicks >= 60)
            await dispatcher.Prepare<LifecycleServerStatusTransitionDoneAction>().DispatchAsync();

        var (transition, status) = (_stateAccessor.State.Transition, _stateAccessor.State.ServerInfo.Status);

        if ((transition == ServerTransition.Starting && status == Domain.Enums.Status.Running) ||
            (transition == ServerTransition.Stopping && status == Domain.Enums.Status.Stopped))
            await dispatcher.Prepare<LifecycleServerStatusTransitionDoneAction>().DispatchAsync();
        else if ((transition == ServerTransition.Starting && status == Domain.Enums.Status.Stopped) ||
                 (transition == ServerTransition.Stopping && status == Domain.Enums.Status.Running))
            await dispatcher.Prepare<LifecycleServerStatusTransitionTickedAction>().DispatchAsync();


    }
}
