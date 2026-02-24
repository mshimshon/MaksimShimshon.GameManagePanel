using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class ServerStatusTransitionDoneReducer : IReducer<ServerState, ServerStatusTransitionDoneAction>
{
    public ServerState Reduce(ServerState state, ServerStatusTransitionDoneAction action)
    {

        var nstate = state with { Delay = 8, Transition = ServerTransition.Idle };
        return nstate;
    }
}
