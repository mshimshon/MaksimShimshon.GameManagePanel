using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class ServerStartDoneReducer : IReducer<ServerState, ServerStartDoneAction>
{
    public ServerState Reduce(ServerState state, ServerStartDoneAction action)
    {
        return state with
        {
            //SkipNextUpdates = 4
            Transition = ServerTransition.Starting,
            Delay = 2
        };
    }
}
