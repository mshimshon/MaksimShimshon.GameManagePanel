using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

public sealed record ModListState : IStateFeatureSingleton
{
    public IEnumerable<ModListDescriptor> Available { get; init; } = new List<ModListDescriptor>().AsReadOnly();
    public bool IsLoadingAvailable { get; init; }
    public IEnumerable<PartSchematicEntity> SchematicParts { get; init; } = new List<PartSchematicEntity>().AsReadOnly();
    /// <summary>
    /// Currently Active ModList select for the server to use at startup.
    /// </summary>
    public ModListDescriptor? Active { get; init; }
    public bool IsActiveLoading { get; init; }


}
