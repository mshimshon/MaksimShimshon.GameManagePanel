using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;

public sealed record ModListEntity
{

    public ModListDescriptor Descriptor { get; }
    private readonly Dictionary<PartId, List<ModEntity>> _mods;

    public IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> Mods =>
           _mods.ToDictionary(p => p.Key, p => (IReadOnlyList<ModEntity>)p.Value.AsReadOnly()).AsReadOnly();

    public ModListEntity(ModListDescriptor descriptor, IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> mods)
    {
        _mods = mods.ToDictionary(p => p.Key, p => p.Value.ToList());
        Descriptor = descriptor;
    }
}
