using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStatusUpdateDoneReducer : IReducer<LifecycleServerState, LifecycleServerStatusUpdateDoneAction>
{
    public async Task<LifecycleServerState> ReduceAsync(LifecycleServerState state, LifecycleServerStatusUpdateDoneAction action)
        => await Task.FromResult(state with { ServerInfoLastUpdate = DateTime.UtcNow, ServerInfo = action.ServerInfo });
}
