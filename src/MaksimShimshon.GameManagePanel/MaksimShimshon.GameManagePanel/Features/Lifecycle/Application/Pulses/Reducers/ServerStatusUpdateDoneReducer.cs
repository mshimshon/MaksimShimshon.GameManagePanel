using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class ServerStatusUpdateDoneReducer : IReducer<ServerState, ServerStatusUpdateDoneAction>
{
    public ServerState Reduce(ServerState state, ServerStatusUpdateDoneAction action)
        => state with
        {
            ServerInfoLastUpdate = DateTime.UtcNow,
            ServerInfo = action.ServerInfo
        };
}
