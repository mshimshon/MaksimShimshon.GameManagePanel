using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands.Handlers;

public class InstallGameServerHandler : HandlerBase, IRequestHandler<InstallGameServerCommand>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public InstallGameServerHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ILogger<InstallGameServerHandler> logger) : base(notificationService, logger)
    {
        Console.WriteLine("Perform Install MEDITR");

        _linuxGameServerService = linuxGameServerService;
    }

    public async Task Handle(InstallGameServerCommand request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(() => _linuxGameServerService.PerformServerInstallation(request.Id, request.DisplayName, cancellationToken));
}
