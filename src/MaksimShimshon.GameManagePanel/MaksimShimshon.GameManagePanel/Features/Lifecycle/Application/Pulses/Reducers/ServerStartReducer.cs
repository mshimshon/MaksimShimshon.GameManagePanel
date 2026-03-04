using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

internal class ServerStartReducer : IReducer<ServerState, ServerStartAction>
{
    public ServerState Reduce(ServerState state, ServerStartAction action)
        => state with
        {
            Transition = States.Enums.ServerTransition.Starting
        };
}
