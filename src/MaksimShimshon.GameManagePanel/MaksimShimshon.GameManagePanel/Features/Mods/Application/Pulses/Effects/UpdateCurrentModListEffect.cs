using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;

internal class UpdateCurrentModListEffect : IEffect<UpdateCurrentModListAction>
{
    public async Task EffectAsync(UpdateCurrentModListAction action, IDispatcher dispatcher)
    {
        // TODO: MEDIHATER

        await dispatcher.Prepare<UpdateCurrentModListDoneAction>()
            .With(p => p.Current, action.Current)
            .DispatchAsync();
    }
}
