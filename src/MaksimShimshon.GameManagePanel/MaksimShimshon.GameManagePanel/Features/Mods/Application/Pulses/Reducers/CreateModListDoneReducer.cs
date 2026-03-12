using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal class CreateModListDoneReducer : IReducer<ModListLocalState, CreateModListDoneAction>
{
    public ModListLocalState Reduce(ModListLocalState state, CreateModListDoneAction action)
        => state with
        {
            Current = action.ModList,
            IsCurrentLoading = false
        };
}
