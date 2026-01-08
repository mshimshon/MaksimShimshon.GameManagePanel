using MaksimShimshon.GameManagePanel.Features.Notification.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.Notification;

internal static class NotificationServiceExt
{
    public static void AddNotificationFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, ToastNotificationService>();

    }
}
