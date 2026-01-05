using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries.Handlers;

internal class GetSystemInfoHandler : HandlerBase, IRequestHandler<GetSystemInfoQuery, SystemInfoEntity?>
{
    private readonly ISystemInfoService _systemInfoService;

    public GetSystemInfoHandler(ISystemInfoService systemInfoService, INotificationService notificationService, ILogger logger) : base(notificationService, logger)
    {
        _systemInfoService = systemInfoService;
    }

    public async Task<SystemInfoEntity?> Handle(GetSystemInfoQuery request, CancellationToken cancellationToken)
    {
        return await ExecAndHandleExceptions(
        () => _systemInfoService.GetSystemInfoAsync(cancellationToken),
        () => default
        );

    }
}
