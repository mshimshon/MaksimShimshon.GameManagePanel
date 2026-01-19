using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class UpdateInstalledGameServerDoneReducer : IReducer<InstallationState, UpdateInstalledGameServerDoneAction>
{
    public InstallationState Reduce(InstallationState state, UpdateInstalledGameServerDoneAction action)
        => state with
        {
            GameServerInfo = action.GameServerInfo,
            IsInstalledGameDiskLoaded = true
        };
}
