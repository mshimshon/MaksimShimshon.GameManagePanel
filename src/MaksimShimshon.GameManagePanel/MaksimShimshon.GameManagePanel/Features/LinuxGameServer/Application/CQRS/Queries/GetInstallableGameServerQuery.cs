using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;

public record GetInstallableGameServerQuery : IRequest<IReadOnlyDictionary<string, string>>
{
}
