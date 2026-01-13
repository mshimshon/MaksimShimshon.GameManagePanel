using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusUpdateSkippedReducer : IReducer<LifecycleServerState, LifecycleServerStatusUpdateSkippedAction>
{
    public LifecycleServerState Reduce(LifecycleServerState state, LifecycleServerStatusUpdateSkippedAction action)
        => state with
        {
            SkipNextUpdates = state.SkipNextUpdates - 1 <= 0 ? 0 : state.SkipNextUpdates - 1
        };
}
