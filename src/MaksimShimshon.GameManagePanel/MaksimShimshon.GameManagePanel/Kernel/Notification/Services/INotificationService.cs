using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;

namespace MaksimShimshon.GameManagePanel.Kernel.Notification.Services;

public interface INotificationService
{
    Task NotifyAsync(string message, NotificationSeverity severity);
}
