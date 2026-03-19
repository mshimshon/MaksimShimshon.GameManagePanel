using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;

public sealed record GetRawGameInfoQuery : IRequest<string?>
{
}
