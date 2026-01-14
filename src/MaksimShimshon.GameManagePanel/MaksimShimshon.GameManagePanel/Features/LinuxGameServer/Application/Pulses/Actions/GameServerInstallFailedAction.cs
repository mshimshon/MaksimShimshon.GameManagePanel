using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public record GameServerInstallFailedAction : IAction
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
