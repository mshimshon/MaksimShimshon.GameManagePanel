using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

internal class ServerStopReducer : IReducer<ServerState, ServerStopAction>
{
    public ServerState Reduce(ServerState state, ServerStopAction action)
    {
        return state with
        {
            Transition = ServerTransition.Stopping,
        };
    }
}