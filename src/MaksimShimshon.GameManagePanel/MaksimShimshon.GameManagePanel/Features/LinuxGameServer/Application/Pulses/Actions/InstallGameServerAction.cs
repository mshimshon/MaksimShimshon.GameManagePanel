using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public record InstallGameServerAction : ISafeAction
{
    public string Id { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
}
