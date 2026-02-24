using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class ServerStopDoneReducer : IReducer<ServerState, ServerStopDoneAction>
{
    public ServerState Reduce(ServerState state, ServerStopDoneAction action)
    {
        return state with
        {
            //SkipNextUpdates = 4
            Transition = ServerTransition.Stopping,
            Delay = 2
        };
    }
}