using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Kernel.CQRS;

public abstract class HandlerBase
{
    protected readonly INotificationService _notificationService;
    protected readonly ILogger _logger;

    protected HandlerBase(INotificationService notificationService, ILogger logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }
    protected virtual async Task ExecAndHandleExceptions(Func<Task> exec)
    {
        try
        {
            await exec();
        }
        catch (WebServiceException ex)
        {
            Console.WriteLine("WebService Error: {0}", ex.Message);
            await _notificationService.NotifyAsync(ex.Message, NotificationSeverity.Error);
            if (ex.Origin != default)
                _logger.LogError(ex, ex.Origin.Message);
            else
                _logger.LogError(ex, ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("WebService Error: {0}", ex.Message);
            await _notificationService.NotifyAsync("Unknown Error, Please contact admins if persistent.", NotificationSeverity.Error);
            _logger.LogError(ex, ex.Message);
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
