using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;

public interface IModListService
{
    Task<ModListEntity?> CreateAsync(ModListDescriptor descriptor, CancellationToken ct = default);
    Task<ModListEntity?> GetAsync(Guid id, CancellationToken ct = default);
}
