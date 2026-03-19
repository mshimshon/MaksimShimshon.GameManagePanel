using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

public sealed record ModListState : IStateFeatureSingleton
{
    public IReadOnlyCollection<ModListDescriptor> Available { get; init; } = new List<ModListDescriptor>().AsReadOnly();
    public bool IsLoadingAvailable { get; init; }
    public IReadOnlyCollection<PartSchematicEntity> SchematicParts { get; init; } = new List<PartSchematicEntity>().AsReadOnly();
    public bool IsSchematicPartsLoading { get; init; }

    /// <summary>
    /// Currently Active ModList select for the server to use at startup.
    /// </summary>
    public ModListDescriptor? Active { get; init; }
    public bool IsActiveLoading { get; init; }


}
