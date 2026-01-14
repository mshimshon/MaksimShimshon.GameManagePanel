using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public record GameServerInstalledAction : IAction
{
    public GameServerInfoEntity GameServerInstalled { get; set; } = default!;
}
