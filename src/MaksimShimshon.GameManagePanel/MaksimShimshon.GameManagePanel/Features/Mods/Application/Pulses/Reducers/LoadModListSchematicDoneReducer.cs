using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal sealed class LoadModListSchematicDoneReducer : IReducer<ModListState, LoadModListSchematicDoneAction>
{
    public ModListState Reduce(ModListState state, LoadModListSchematicDoneAction action)
        => state with
        {
            IsSchematicPartsLoading = false,
            SchematicParts = action.SchematicParts ?? new List<PartSchematicEntity>().AsReadOnly()
        };
}
