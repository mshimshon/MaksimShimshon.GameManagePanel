using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusTransitionDoneReducer : IReducer<LifecycleServerState, LifecycleServerStatusTransitionDoneAction>
{
    public LifecycleServerState Reduce(LifecycleServerState state, LifecycleServerStatusTransitionDoneAction action)
    {

        var nstate = state with { Delay = 8, Transition = ServerTransition.Idle, TransitionTicks = 0 };
        return nstate;
    }
}
