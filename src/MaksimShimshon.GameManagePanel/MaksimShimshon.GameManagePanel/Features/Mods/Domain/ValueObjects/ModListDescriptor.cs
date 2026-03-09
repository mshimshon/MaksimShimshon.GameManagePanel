using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions.Extensions;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

public sealed record ModListDescriptor
{
    public Guid Id { get; }
    public string Name { get; }
    public ModListDescriptor(Guid id, string name)
    {
        Id = id;
        Name = name;
        Name.ThrowIfNullOrWhiteSpace<ModListDescriptor>(nameof(Name));
    }
}
