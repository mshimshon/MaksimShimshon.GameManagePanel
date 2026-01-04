using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusUpdateSkippedReducer : IReducer<LifecycleServerState, LifecycleServerStatusUpdateSkippedAction>
{
    public async Task<LifecycleServerState> ReduceAsync(LifecycleServerState state, LifecycleServerStatusUpdateSkippedAction action)
        => await Task.FromResult(state with { 
            SkipNextUpdates = state.SkipNextUpdates - 1 <= 0 ? 0 : state.SkipNextUpdates - 1 
        });
}
