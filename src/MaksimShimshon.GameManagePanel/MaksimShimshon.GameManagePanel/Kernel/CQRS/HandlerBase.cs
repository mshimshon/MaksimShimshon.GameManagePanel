using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;

namespace MaksimShimshon.GameManagePanel.Kernel.CQRS;

public abstract class HandlerBase
{
    protected readonly INotificationService _notificationService;
    private readonly ICrazyReport _crazyReport;

    protected HandlerBase(INotificationService notificationService, ICrazyReport crazyReport)
    {
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(KernelModule.ModuleName);
    }
    protected virtual async Task ExecAndHandleExceptions(Func<Task> exec, Action<WebServiceException>? onError = default)
    {
        try
        {
            await exec();
        }
        catch (WebServiceException ex)
        {
            _crazyReport.ReportError(ex.Message);
            await _notificationService.NotifyAsync(ex.Message, NotificationSeverity.Error);
            if (ex.Origin != default)
                _crazyReport.ReportError(ex.Origin.Message);

            if (onError != default)
                onError.Invoke(ex);
        }
        catch (Exception ex)
        {
            await _notificationService.NotifyAsync("Unknown Error, Please contact admins if persistent.", NotificationSeverity.Error); // TODO: Localize
            _crazyReport.ReportError(ex.Message);
            if (onError != default)
                onError.Invoke(new("Unknown Error, Please contact admins if persistent.", ex)); // TODO: Localize
        }
    }


    protected async Task<TResult> ExecAndHandleExceptions<TResult>(Func<Task<TResult>> exec, Func<TResult> onError)
    {
        TResult? result = default;
        await ExecAndHandleExceptions(async () =>
        {
            result = await exec();
        });
        return result ?? onError();
    }
}
