using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;

internal class CreateModListReducer : IReducer<ModListLocalState, CreateModListAction>
{
    public ModListLocalState Reduce(ModListLocalState state, CreateModListAction action) => state with
    {
        IsCurrentLoading = true
    };
}
