using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;

public class GetServerStatusHandler : HandlerBase, IRequestHandler<GetServerStatusQuery, ServerInfoEntity?>
{
    private readonly ILifecycleServices _lifecycleServices;

    public GetServerStatusHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<GetServerStatusHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task<ServerInfoEntity?> Handle(GetServerStatusQuery request, CancellationToken cancellationToken)
    {
        return
            await ExecAndHandleExceptions(
                () => _lifecycleServices.ServerStatusAsync(),
                () => default
                );

    }
}
