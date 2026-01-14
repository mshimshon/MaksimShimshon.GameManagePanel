using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

public record GameServerInstallStateLoadedAction(GameServerInfoEntity Info) : IAction
{
}
