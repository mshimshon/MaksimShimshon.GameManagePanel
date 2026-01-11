using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands;

public record InstallGameServerCommand(string Id) : IRequest<GameServerInfoEntity?>
{
}
