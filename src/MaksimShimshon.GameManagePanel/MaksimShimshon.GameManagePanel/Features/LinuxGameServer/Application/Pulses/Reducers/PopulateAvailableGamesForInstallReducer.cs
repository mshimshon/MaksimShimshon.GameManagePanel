using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class PopulateAvailableGamesForInstallReducer : IReducer<InstallationState, PopulateAvailableGamesForInstallAction>
{
    public InstallationState Reduce(InstallationState state, PopulateAvailableGamesForInstallAction action)
        => state with
        {
            AvailableGameServers = action.AvailableGameServer.AsReadOnly()
        };
}
