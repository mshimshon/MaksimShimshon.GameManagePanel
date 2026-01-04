using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerGameInfoUpdatedReducer : IReducer<LifecycleGameInfoState, LifecycleServerGameInfoUpdatedAction>
{

    public LifecycleServerGameInfoUpdatedReducer()
    {
    }
    public Task<LifecycleGameInfoState> ReduceAsync(LifecycleGameInfoState state, LifecycleServerGameInfoUpdatedAction action) => Task.FromResult(state with { GameInfo = action.GameInfo });
}
