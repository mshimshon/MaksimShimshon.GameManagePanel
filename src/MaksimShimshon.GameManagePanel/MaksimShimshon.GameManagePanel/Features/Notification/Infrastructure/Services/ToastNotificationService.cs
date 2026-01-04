using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Notification.Infrastructure.Services;

internal class ToastNotificationService : IToastNotificationService
{
    private readonly IDispatcher _dispatcher;

    public ToastNotificationService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    public async Task ToastAsync(string message, ToastColor severity)
    {
        await _dispatcher.Prepare<SendToastNotificationAction>()
        .With(p => p.Message, message)
        .With(p => p.Color, severity)
        .DispatchAsync();
    }
}
