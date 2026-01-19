using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;

public sealed record GetInstalledGameQuery : IRequest<GameServerInfoEntity?>
{
}
