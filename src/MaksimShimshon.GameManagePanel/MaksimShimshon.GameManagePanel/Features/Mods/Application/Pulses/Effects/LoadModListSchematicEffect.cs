using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;

internal sealed class LoadModListSchematicEffect : IEffect<LoadModListSchematicAction>
{
    private readonly IMedihater _medihater;
    private readonly INotificationService _notificationService;

    public LoadModListSchematicEffect(IMedihater medihater, INotificationService notificationService)
    {
        _medihater = medihater;
        _notificationService = notificationService;
    }
    public async Task EffectAsync(LoadModListSchematicAction action, IDispatcher dispatcher)
    {
        var query = new GetModSchematicQuery();
        var result = await _medihater.Send(query);
        if (result == default)
        {
            await _notificationService.NotifyAsync($"Cannot Load Mod Schematic for some reasons", NotificationSeverity.Error);
            result = new List<PartSchematicEntity>();
        }

        await dispatcher
            .Prepare<LoadModListSchematicDoneAction>()
            .With(p => p.SchematicParts, result)
            .DispatchAsync();
    }
}
