using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerGameInfoUpdatedReducer : IReducer<LifecycleGameInfoState, LifecycleServerGameInfoUpdateDoneAction>
{

    public LifecycleGameInfoState Reduce(LifecycleGameInfoState state, LifecycleServerGameInfoUpdateDoneAction action)
        => state with
        {
            GameInfo = action.GameInfo
        };
}
