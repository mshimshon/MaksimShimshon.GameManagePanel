using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusTransitionTickedReducer : IReducer<LifecycleServerState, LifecycleServerStatusTransitionTickedAction>
{
    public LifecycleServerState Reduce(LifecycleServerState state, LifecycleServerStatusTransitionTickedAction action)
        => state with
        {
            TransitionTicks = state.TransitionTicks + 1
        };
}
