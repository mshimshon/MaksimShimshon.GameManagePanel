using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;

public class GetStartupParametersHandler : HandlerBase, IRequestHandler<GetStartupParametersQuery, Dictionary<string, string>>
{
    private readonly ILifecycleServices _lifecycleServices;
    public GetStartupParametersHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<GetStartupParametersHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task<Dictionary<string, string>> Handle(GetStartupParametersQuery request, CancellationToken cancellationToken)
    {
        return await ExecAndHandleExceptions(
            () => _lifecycleServices.GetServerStartupParametersAsync(cancellationToken),
            () => new()
            );
    }
}
