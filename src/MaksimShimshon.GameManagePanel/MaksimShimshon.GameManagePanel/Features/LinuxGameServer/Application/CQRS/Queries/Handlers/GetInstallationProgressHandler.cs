using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;

internal class GetInstallationProgressHandler : HandlerBase, IRequestHandler<GetInstallationProgressQuery, GameServerInstallProcessModel?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public GetInstallationProgressHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ILogger<GetInstallationProgressHandler> logger)
        : base(notificationService, logger)
    {
        _linuxGameServerService = linuxGameServerService;
    }

    public async Task<GameServerInstallProcessModel?> Handle(GetInstallationProgressQuery request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(
        () => _linuxGameServerService.GetInstallationProgress(cancellationToken),
        () =>
        {
            Console.WriteLine($"{nameof(GetInstallationProgressHandler)} :: Error ");
            return default;
        }
        );
}
