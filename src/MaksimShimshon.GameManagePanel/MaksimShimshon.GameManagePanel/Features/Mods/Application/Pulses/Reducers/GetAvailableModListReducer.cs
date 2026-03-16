using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal sealed class GetAvailableModListReducer : IReducer<ModListState, GetAvailableModListAction>
{
    public ModListState Reduce(ModListState state, GetAvailableModListAction action)
        => state with
        {
            IsLoadingAvailable = true
        };
}
