using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;

public sealed record LoadModListSchematicDoneAction : IAction
{
    public IReadOnlyCollection<PartSchematicEntity>? SchematicParts { get; set; }
}
