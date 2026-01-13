using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class GameServerInstallStateLoadedReducer : IReducer<InstallationState, GameServerInstallStateLoadedAction>
{
    public InstallationState Reduce(InstallationState state, GameServerInstallStateLoadedAction action)
        => state with
        {
            GameServerInfo = action.Info
        };
}
