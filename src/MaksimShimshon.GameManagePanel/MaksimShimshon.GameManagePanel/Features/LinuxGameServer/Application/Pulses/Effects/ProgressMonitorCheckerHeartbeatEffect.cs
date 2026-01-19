using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.ConsoleController;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;

internal class ProgressMonitorCheckerHeartbeatEffect : IEffect<HeartbeatRunnerAction>
{
    private readonly IStateAccessor<InstallationState> _installStateAccess;
    private readonly ILinuxGameServerInstallationRefresherService _installationRefresherService;
    private readonly ICrazyReport _crazyReport;

    public ProgressMonitorCheckerHeartbeatEffect(
        IStateAccessor<InstallationState> installStateAccess,
        ILinuxGameServerInstallationRefresherService installationRefresherService, ICrazyReport crazyReport)
    {
        _installStateAccess = installStateAccess;
        _installationRefresherService = installationRefresherService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(LinuxGameServerModule.ModuleName);
    }
    public async Task EffectAsync(HeartbeatRunnerAction action, IDispatcher dispatcher)
    {
        _crazyReport.ReportInfo("{0} Running Heartbeat", nameof(ProgressMonitorCheckerHeartbeatEffect));
        /*
         * Core logic:
         * 1. If the game server is NOT installed, monitor the files that indicate
         *    installation progress or the presence of an installed game. This ensures
         *    the dashboard receives up‑to‑date information quickly.
         *
         * 2. Stop monitoring only after the server is successfully installed.
         *    Until then, continuously reload the files into state.
         *
         * This design prevents mismatches caused by background automation scripts.
         * The files act as the single source of truth, and the bash scripts are
         * responsible for generating them.
         */
        if (!_installStateAccess.State.IsInstallationCompleted)
        {
            if (!_installationRefresherService.IsRunning)
            {
                _crazyReport.ReportInfo("{0} Monitoring of Game Installation is Requested", nameof(ProgressMonitorCheckerHeartbeatEffect));
                await _installationRefresherService.MonitorProgressFileAsync();
            }

            if (_installStateAccess.State.IsInstallationCompleted)
                await _installationRefresherService.StopMonitoringFileAsync();
        }
    }
}
