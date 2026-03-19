using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecRestartServerHandler : HandlerBase, IRequestHandler<ExecRestartServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;

    public ExecRestartServerHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ICrazyReport<ExecRestartServerHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
        logger.SetModule(LifecycleKeys.ModuleName);
    }
    public async Task Handle(ExecRestartServerCommand request, CancellationToken cancellationToken)
    {
        await ExecAndHandleExceptions(
            () => _lifecycleServices.ServerRestartAsync(cancellationToken)
            );
    }
}
