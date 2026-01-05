using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecStartServerHandler : HandlerBase, IRequestHandler<ExecStartServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;

    public ExecStartServerHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<ExecStartServerHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task Handle(ExecStartServerCommand request, CancellationToken cancellationToken)
    {
        await ExecAndHandleExceptions(
            () => _lifecycleServices.ServerStartAsync(cancellationToken)
            );
    }
}
