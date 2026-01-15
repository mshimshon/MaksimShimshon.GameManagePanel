using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public record ReceivingUpdatedInstallStateAction : IAction
{
    public InstallationState NewState { get; set; }
    public long NewTick { get; set; }
}
