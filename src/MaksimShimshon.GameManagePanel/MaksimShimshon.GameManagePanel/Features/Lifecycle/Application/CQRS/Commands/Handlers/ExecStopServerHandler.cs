using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecStopServerHandler : HandlerBase, IRequestHandler<ExecStopServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;

    public ExecStopServerHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ICrazyReport<ExecStopServerHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
        logger.SetModule(LifecycleKeys.ModuleName);
    }
    public async Task Handle(ExecStopServerCommand request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(() => _lifecycleServices.ServerStopAsync(cancellationToken));
}
