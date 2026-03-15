using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
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
        try
        {
            var command = new CreateModListCommand(action.Id, action.Name);
            await _medihater.Send(command);
            await dispatcher.Prepare<CreateModListDoneAction>().DispatchAsync();
        }
        catch (WebServiceException)
        {
            await dispatcher.Prepare<CreateModListDoneAction>()
                .With(p => p.Failed, true)
                .DispatchAsync();
        }
        catch (Exception)
        {
            await dispatcher.Prepare<CreateModListDoneAction>()
                .With(p => p.Failed, true)
                .DispatchAsync();
            // TODO: LOG
        }
    }
}
