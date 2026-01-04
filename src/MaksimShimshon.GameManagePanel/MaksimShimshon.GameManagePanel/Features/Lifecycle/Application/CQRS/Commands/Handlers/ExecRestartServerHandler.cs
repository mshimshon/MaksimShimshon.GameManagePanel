using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecRestartServerHandler : HandlerBase, IRequestHandler<ExecRestartServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;

    public ExecRestartServerHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<ExecRestartServerHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task Handle(ExecRestartServerCommand request, CancellationToken cancellationToken)
    {
        await ExecAndHandleExceptions(
            () => _lifecycleServices.ServerRestartAsync(cancellationToken)
            );
    }
}
