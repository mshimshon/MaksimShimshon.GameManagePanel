using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;

public sealed record ModEntity
{
    public ModId Id { get; }
    public ModName? Name { get; init; }
    private List<ModEntity> _dependencies;
    public IEnumerable<ModEntity>? Dependencies => _dependencies.AsReadOnly();
    public ModEntity(ModId id, IEnumerable<ModEntity>? dependencies = default)
    {
        Id = id;
        _dependencies = dependencies?.ToList() ?? new();
    }
}
