using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleServerStopDoneReducer : IReducer<LifecycleServerState, LifecycleServerStopDoneAction>
{
    public LifecycleServerState Reduce(LifecycleServerState state, LifecycleServerStopDoneAction action)
    {
        return state with
        {
            //SkipNextUpdates = 4
            Transition = ServerTransition.Stopping,
            Delay = 2
        };
    }
}