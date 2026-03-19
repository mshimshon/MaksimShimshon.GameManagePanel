using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;

internal class GetInstallationProgressHandler : HandlerBase, IRequestHandler<GetInstallationProgressQuery, GameServerInstallProcessModel?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public GetInstallationProgressHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ICrazyReport<GetInstallationProgressHandler> logger)
        : base(notificationService, logger)
    {
        _linuxGameServerService = linuxGameServerService;
        logger.SetModule(LinuxGameServerKeys.ModuleName);
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
