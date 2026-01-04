using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;

namespace MaksimShimshon.GameManagePanel.Kernel.Notification.Api;

public interface IToastNotificationService
{
    Task ToastAsync(string message, ToastColor severity);
}
