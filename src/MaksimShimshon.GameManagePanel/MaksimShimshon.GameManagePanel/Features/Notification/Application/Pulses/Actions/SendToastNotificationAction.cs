using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;

public record SendToastNotificationAction : IAction
{
    public string Message { get; set; } = default!;
    public NotificationSeverity Color { get; set; } = NotificationSeverity.Info;
}
