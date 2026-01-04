using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusTransitionDoneReducer : IReducer<LifecycleServerState, LifecycleServerStatusTransitionDoneAction>
{
    public async Task<LifecycleServerState> ReduceAsync(LifecycleServerState state, LifecycleServerStatusTransitionDoneAction action)
    {

        var nstate = state with { Delay = 8, Transition = ServerTransition.Idle, TransitionTicks = 0 };
        return nstate;
    }
}
