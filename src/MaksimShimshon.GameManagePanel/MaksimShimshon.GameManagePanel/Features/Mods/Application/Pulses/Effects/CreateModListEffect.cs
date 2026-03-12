using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;

internal sealed class CreateModListEffect : IEffect<CreateModListAction>
{
    private readonly IMedihater _medihater;

    public CreateModListEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(CreateModListAction action, IDispatcher dispatcher)
    {
        var command = new CreateModListCommand(action.Id, action.Name);
        var result = await _medihater.Send(command);
        Console.WriteLine($"SDASFDHADSFISHDFOIJS: {(result == default)}");
        await dispatcher
            .Prepare<CreateModListDoneAction>()
            .With(p => p.ModList, result)
            .DispatchAsync();
    }
}
