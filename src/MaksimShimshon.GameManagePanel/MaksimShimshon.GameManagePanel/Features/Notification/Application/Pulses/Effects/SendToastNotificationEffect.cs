using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Effects;

public class SendToastNotificationEffect : IEffect<SendToastNotificationAction>
{
    private readonly ISnackbar _snackbar;

    public SendToastNotificationEffect(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }
    public Task EffectAsync(SendToastNotificationAction action, IDispatcher dispatcher)
    {
        Severity selectedSeverity = action.Color switch
        {
            ToastColor.Error => Severity.Error,
            ToastColor.Warning => Severity.Warning,
            ToastColor.Success => Severity.Success,
            _ => Severity.Info
        };
        _snackbar.Add((MarkupString)action.Message, selectedSeverity);
        return Task.CompletedTask;
    }
}
