using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecStopServerHandler : HandlerBase, IRequestHandler<ExecStopServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;

    public ExecStopServerHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<ExecStopServerHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task Handle(ExecStopServerCommand request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(() => _lifecycleServices.ServerStopAsync(cancellationToken));
}
