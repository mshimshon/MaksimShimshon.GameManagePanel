using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;

internal sealed class GetGameInfoHandler : HandlerBase, IRequestHandler<GetGameInfoQuery, GameInfoEntity?>
{
    private readonly ILifecycleServices _lifecycleServices;

    public GetGameInfoHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<GetGameInfoHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task<GameInfoEntity?> Handle(GetGameInfoQuery request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                () => _lifecycleServices.LoadGameInfoAsync(cancellationToken),
                () => default
                );
}
