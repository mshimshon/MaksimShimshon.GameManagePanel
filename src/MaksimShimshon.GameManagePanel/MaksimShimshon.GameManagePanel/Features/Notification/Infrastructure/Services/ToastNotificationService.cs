using LunaticPanel.Core.Abstraction.Circuit;
using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Notification.Infrastructure.Services;

internal sealed class ToastNotificationService : INotificationService
{
    private readonly IDispatcher _dispatcher;
    private readonly ICircuitRegistry _circuitRegistry;

    public ToastNotificationService(IDispatcher dispatcher, ICircuitRegistry circuitRegistry)
    {
        _dispatcher = dispatcher;
        _circuitRegistry = circuitRegistry;
    }
    public async Task NotifyAsync(string message, NotificationSeverity severity)
    {
        if (_circuitRegistry.CurrentCircuit == default || _circuitRegistry.CurrentCircuit.IsMaster)
            return;
        await _dispatcher.Prepare<SendToastNotificationAction>()
        .With(p => p.Message, message)
        .With(p => p.Color, severity)
        .DispatchAsync();
    }
}
