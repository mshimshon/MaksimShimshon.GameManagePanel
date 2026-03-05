using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;

public sealed record PartSchematicEntity
{
    public PartId Id { get; }
    public PartName Name { get; }
    public PartType Type { get; }
    public PartSchematicEntity(string id, string name, string type)
    {
        Id = new(id);
        Name = new(name);
        Type = new(type);
    }
}
