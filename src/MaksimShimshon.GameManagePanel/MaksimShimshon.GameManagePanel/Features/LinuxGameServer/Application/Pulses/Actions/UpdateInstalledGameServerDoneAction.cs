using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateInstalledGameServerDoneAction : IAction
{
    public GameServerInfoEntity? GameServerInfo { get; set; }
}
