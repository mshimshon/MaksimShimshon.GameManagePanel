using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class UpdateProgressStateFromDiskDoneReducer : IReducer<InstallationState, UpdateProgressStateFromDiskDoneAction>
{
    public InstallationState Reduce(InstallationState state, UpdateProgressStateFromDiskDoneAction action)
        => state with
        {
            InProgressInstallation = action.ProgressState,
            IsProgressDiskLoaded = true
        };
}
