using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class InstallGameServerActionReducer : IReducer<InstallationState, InstallGameServerAction>
{

    public InstallationState Reduce(InstallationState state, InstallGameServerAction action)
     => state with
     {
         InProgressInstallation = new()
         {
             CurrentStep = $"Installing ${action.DisplayName}...",
             IsInstalling = true
         }
     };
}
