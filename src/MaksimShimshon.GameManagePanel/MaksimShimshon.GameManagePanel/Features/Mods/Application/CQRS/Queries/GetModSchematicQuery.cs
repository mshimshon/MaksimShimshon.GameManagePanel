using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries;

public sealed record GetModSchematicQuery : IRequest<IReadOnlyCollection<PartSchematicEntity>?>
{
}
