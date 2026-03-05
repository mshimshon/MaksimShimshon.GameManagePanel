using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions.Extensions;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

public sealed record ModListDescriptor : BaseId<ModListDescriptor>
{
    public string Name { get; }
    public ModListDescriptor(string id, string name) : base(id)
    {
        Name = name;
        Name.ThrowIfNullOrWhiteSpace<ModListDescriptor>(nameof(Name));
    }
}
