using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Queries.Handlers;

public class GetStartupParametersHandler : IRequestHandler<GetStartupParametersQuery, Dictionary<string, string>>
{
    private readonly ILifecycleServices _lifecycleServices;
    private readonly IToastNotificationService _toastNotificationService;
    private readonly ILogger<GetStartupParametersHandler> _logger;
    public GetStartupParametersHandler(ILifecycleServices lifecycleServices, IToastNotificationService toastNotificationService, ILogger<GetStartupParametersHandler> logger)
    {
        _lifecycleServices = lifecycleServices;
        _toastNotificationService = toastNotificationService;
        _logger = logger;
    }
    public async Task<Dictionary<string, string>> Handle(GetStartupParametersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _lifecycleServices.GetServerStartupParametersAsync(cancellationToken);
        }
        catch (WebServiceException ex)
        {
            await _toastNotificationService.ToastAsync(ex.Message, ToastColor.Error);
            if (ex.Origin != default)
                _logger.LogError(ex, ex.Origin.Message);
            return new();
        }
        catch (Exception ex)
        {
            await _toastNotificationService
                .ToastAsync("Unknown Error, Please contact admins if persistent.", ToastColor.Error);
            _logger.LogError(ex, ex.Message);
            return new();
        }
    }
}
