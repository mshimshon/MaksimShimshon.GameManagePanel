using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;

internal class GetInstalledGameHandler : HandlerBase, IRequestHandler<GetInstalledGameQuery, GameServerInfoEntity?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public GetInstalledGameHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ILogger<GetInstalledGameHandler> logger) : base(notificationService, logger)
    {
        _linuxGameServerService = linuxGameServerService;
    }

    public async Task<GameServerInfoEntity?> Handle(GetInstalledGameQuery request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(
        () => _linuxGameServerService.GetInstalledGameServer(cancellationToken),
        () => default
        );
}
