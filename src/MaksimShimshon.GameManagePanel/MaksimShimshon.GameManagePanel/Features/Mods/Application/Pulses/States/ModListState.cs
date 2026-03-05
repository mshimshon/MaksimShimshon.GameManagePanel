using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

public sealed record ModListState : IStateFeatureSingleton
{
    public IEnumerable<ModListEntity> Available { get; init; } = new List<ModListEntity>().AsReadOnly();
    public IEnumerable<PartSchematicEntity> SchematicParts { get; init; } = new List<PartSchematicEntity>().AsReadOnly();
    public ModListEntity? Current { get; init; }


}
