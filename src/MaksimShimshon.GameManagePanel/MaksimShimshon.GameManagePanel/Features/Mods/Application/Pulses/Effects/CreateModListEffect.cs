using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;

internal sealed class CreateModListEffect : IEffect<CreateModListAction>
{
    private readonly IMedihater _medihater;
    private readonly INotificationService _notificationService;

    public CreateModListEffect(IMedihater medihater, INotificationService notificationService)
    {
        _medihater = medihater;
        _notificationService = notificationService;
    }
    public async Task EffectAsync(CreateModListAction action, IDispatcher dispatcher)
    {
        try
        {
            var command = new CreateModListCommand(action.Id, action.Name);
            await _medihater.Send(command);
            await _notificationService.NotifyAsync($"The Modlist {action.Name} was successfully created.", NotificationSeverity.Success); // TODO: Localize
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
