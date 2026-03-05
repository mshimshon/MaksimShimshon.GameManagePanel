using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions.Extensions;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

public sealed record PartName
{
    public string Name { get; }
    public PartName(string name)
    {
        Name = name;
        name.ThrowIfNullOrWhiteSpace<PartName>(nameof(Name));
    }

}
