using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Queries.Handlers;

public class GetServerStatusHandler : IRequestHandler<GetServerStatusQuery, ServerInfoEntity?>
{
    private readonly ILifecycleServices _lifecycleServices;
    private readonly IToastNotificationService _toastNotificationService;
    private readonly ILogger<GetServerStatusHandler> _logger;

    public GetServerStatusHandler(ILifecycleServices lifecycleServices, IToastNotificationService toastNotificationService, ILogger<GetServerStatusHandler> logger)
    {
        _lifecycleServices = lifecycleServices;
        _toastNotificationService = toastNotificationService;
        _logger = logger;
    }
    public async Task<ServerInfoEntity?> Handle(GetServerStatusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _lifecycleServices.ServerStatusAsync();
        }
        catch (WebServiceException ex)
        {
            await _toastNotificationService.ToastAsync(ex.Message, ToastColor.Error);

            if (ex.Origin != default)
                _logger.LogError(ex, ex.Origin.Message);

            return default;
        }
        catch (Exception ex)
        {
            await _toastNotificationService
                .ToastAsync("Unknown Error, Please contact admins if persistent.", ToastColor.Error);

            _logger.LogError(ex, ex.Message);

            return default;
        }

    }
}
