using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries;

internal sealed record GetModListQuery(Guid Id) : IRequest<ModListEntity>;
