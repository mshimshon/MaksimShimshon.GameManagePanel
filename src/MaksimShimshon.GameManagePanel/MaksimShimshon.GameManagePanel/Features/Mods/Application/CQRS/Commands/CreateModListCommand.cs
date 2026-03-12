using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands;

internal sealed record CreateModListCommand(Guid Id, string Name) : IRequest<ModListEntity?>;
