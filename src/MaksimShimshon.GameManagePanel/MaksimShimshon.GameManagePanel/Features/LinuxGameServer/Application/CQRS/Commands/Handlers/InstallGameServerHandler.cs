using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands.Handlers;

public class InstallGameServerHandler : HandlerBase, IRequestHandler<InstallGameServerCommand>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public InstallGameServerHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ICrazyReport<InstallGameServerHandler> logger) : base(notificationService, logger)
    {
        _linuxGameServerService = linuxGameServerService;
        logger.SetModule(LinuxGameServerKeys.ModuleName);
    }

    public async Task Handle(InstallGameServerCommand request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(() => _linuxGameServerService.PerformServerInstallation(request.Id, request.DisplayName, cancellationToken));
}
