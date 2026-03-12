using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services;

internal sealed class ModListService : IModListService
{
    public Task<ModListEntity?> CreateAsync(ModListDescriptor descriptor, CancellationToken ct = default) => throw new NotImplementedException();
    public Task<ModListEntity?> GetAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult<ModListEntity?>(default);
}
