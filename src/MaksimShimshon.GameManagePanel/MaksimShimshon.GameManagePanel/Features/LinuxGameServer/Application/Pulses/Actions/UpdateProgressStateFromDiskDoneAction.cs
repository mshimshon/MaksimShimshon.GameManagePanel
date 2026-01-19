using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateProgressStateFromDiskDoneAction : IAction
{
    public GameServerInstallProcessModel? ProgressState { get; set; }
}
