using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;

public sealed record GetInstallationProgressQuery : IRequest<GameServerInstallProcessModel>
{
}
