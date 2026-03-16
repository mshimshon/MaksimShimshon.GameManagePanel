using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal sealed class GetAvailableModListDoneReducer : IReducer<ModListState, GetAvailableModListDoneAction>
{
    public ModListState Reduce(ModListState state, GetAvailableModListDoneAction action)
        => state with
        {
            Available = action.Available,
            IsLoadingAvailable = false
        };
}
