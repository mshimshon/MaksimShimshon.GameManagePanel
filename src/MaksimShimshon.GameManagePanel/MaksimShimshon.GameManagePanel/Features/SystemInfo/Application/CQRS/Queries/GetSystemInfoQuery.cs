using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries;

public record GetSystemInfoQuery : IRequest<SystemInfoEntity?>
{
}
