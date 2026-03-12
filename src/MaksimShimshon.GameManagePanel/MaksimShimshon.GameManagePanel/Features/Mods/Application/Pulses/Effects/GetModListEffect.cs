using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;

internal class GetModListEffect : IEffect<GetModListAction>
{
    private readonly IMedihater _medihater;
    private readonly INotificationService _notificationService;

    public GetModListEffect(IMedihater medihater, INotificationService notificationService)
    {
        _medihater = medihater;
        _notificationService = notificationService;
    }
    public async Task EffectAsync(GetModListAction action, IDispatcher dispatcher)
    {
        var query = new GetModListQuery(action.Id);
        var result = await _medihater.Send(query);
        if (result == default)
            await _notificationService.NotifyAsync($"Cannot Load ModList {action.Id} for some reasons", NotificationSeverity.Error);

        await dispatcher
            .Prepare<GetModListDoneAction>()
            .With(p => p.ModList, result)
            .DispatchAsync();
    }
}
