using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal class UpdateCurrentModListDoneReducer : IReducer<ModListState, UpdateCurrentModListDoneAction>
{
    public ModListState Reduce(ModListState state, UpdateCurrentModListDoneAction action)
        => state with
        {
            Current = action.Current,
            IsCurrentLoading = false
        };
}
