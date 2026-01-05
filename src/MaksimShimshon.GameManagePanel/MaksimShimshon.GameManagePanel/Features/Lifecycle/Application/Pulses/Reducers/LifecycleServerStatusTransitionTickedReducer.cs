using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusTransitionTickedReducer : IReducer<LifecycleServerState, LifecycleServerStatusTransitionTickedAction>
{
    public async Task<LifecycleServerState> ReduceAsync(LifecycleServerState state, LifecycleServerStatusTransitionTickedAction action)
        => await Task.FromResult(state with { TransitionTicks = state.TransitionTicks + 1 });
}
