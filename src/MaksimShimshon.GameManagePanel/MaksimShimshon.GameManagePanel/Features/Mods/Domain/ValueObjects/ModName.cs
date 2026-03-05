using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions.Extensions;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

public sealed record ModName
{
    public string Name { get; }
    public ModName(string name)
    {
        Name = name;
        Name.ThrowIfNullOrWhiteSpace<ModName>(nameof(Name));
    }

}
