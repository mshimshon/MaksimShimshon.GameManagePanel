using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.Notification.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Notification;

internal static class NotificationServiceExt
{
    public static void AddNotificationFeatureServices(this IServiceCollection services)
    {
        services.AddStatePulseService<SendToastNotificationAction>();
        services.AddStatePulseService<SendToastNotificationEffect>();
        services.AddScoped<INotificationService, ToastNotificationService>();

    }
}
