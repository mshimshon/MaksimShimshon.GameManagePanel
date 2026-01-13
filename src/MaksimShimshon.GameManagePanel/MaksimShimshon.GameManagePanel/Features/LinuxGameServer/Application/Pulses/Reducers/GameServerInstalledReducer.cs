using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class GameServerInstalledReducer : IReducer<InstallationState, GameServerInstalledAction>
{
    public InstallationState Reduce(InstallationState state, GameServerInstalledAction action)
       => state with
       {
           GameServerInfo = new()
           {
               Id = action.GameServerInstalled.Id,
               InstallDate = action.GameServerInstalled.InstallDate
           },
           InProgressInstallation = default
       };
}
