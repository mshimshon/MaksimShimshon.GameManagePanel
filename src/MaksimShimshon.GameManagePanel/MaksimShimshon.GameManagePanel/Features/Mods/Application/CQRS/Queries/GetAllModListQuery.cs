using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries;

internal sealed record GetAllModListQuery : IRequest<ICollection<ModListDescriptor>>
{
}
