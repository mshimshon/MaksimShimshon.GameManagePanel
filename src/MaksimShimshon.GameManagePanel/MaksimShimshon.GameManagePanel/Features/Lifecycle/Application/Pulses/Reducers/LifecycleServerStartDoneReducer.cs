using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStartDoneReducer : IReducer<LifecycleServerState, LifecycleServerStartDoneAction>
{
    public async Task<LifecycleServerState> ReduceAsync(LifecycleServerState state, LifecycleServerStartDoneAction action) {
        return await Task.FromResult(state with
        {
            //SkipNextUpdates = 4
            Transition = ServerTransition.Starting, 
            Delay = 2
        });
    }
}
