using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;

internal class GetInstalledGameHandler : HandlerBase, IRequestHandler<GetInstalledGameQuery, GameServerInfoEntity?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public GetInstalledGameHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ICrazyReport<GetInstalledGameHandler> logger) : base(notificationService, logger)
    {
        _linuxGameServerService = linuxGameServerService;
        logger.SetModule(LinuxGameServerKeys.ModuleName);
    }

    public async Task<GameServerInfoEntity?> Handle(GetInstalledGameQuery request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(
        () => _linuxGameServerService.GetInstalledGameServer(cancellationToken),
        () => default
        );
}
