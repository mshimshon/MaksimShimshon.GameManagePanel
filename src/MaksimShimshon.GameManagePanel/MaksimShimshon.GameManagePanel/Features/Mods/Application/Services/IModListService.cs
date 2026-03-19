using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;

public interface IModListService
{
    Task CreateAsync(ModListDescriptor descriptor, CancellationToken ct = default);
    Task<ModListEntity?> GetAsync(Guid id, CancellationToken ct = default);
    Task<ICollection<ModListDescriptor>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyCollection<PartSchematicEntity>?> GetSchematic(CancellationToken ct = default);
}
