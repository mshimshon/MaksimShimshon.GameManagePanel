using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal class UpdateCurrentModlistReducer : IReducer<ModListState, UpdateCurrentModListAction>
{
    public ModListState Reduce(ModListState state, UpdateCurrentModListAction action)
        => state with
        {
            IsActiveLoading = true
        };
}
