using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class ReceivingInstallationStateReducer : IReducer<InstallationState, ReceivingUpdatedInstallStateAction>
{
    public InstallationState Reduce(InstallationState state, ReceivingUpdatedInstallStateAction action)
     => action.NewState with { BusVersion = state.BusVersion };
}
