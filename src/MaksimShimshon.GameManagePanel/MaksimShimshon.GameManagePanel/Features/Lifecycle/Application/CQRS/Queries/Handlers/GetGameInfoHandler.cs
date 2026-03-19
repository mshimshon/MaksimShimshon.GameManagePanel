using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;

internal sealed class GetGameInfoHandler : HandlerBase, IRequestHandler<GetGameInfoQuery, GameInfoEntity?>
{
    private readonly IGameInfoService _gameInfoService;

    public GetGameInfoHandler(IGameInfoService gameInfoService, INotificationService notificationService, ICrazyReport<GetGameInfoHandler> logger) : base(notificationService, logger)
    {
        _gameInfoService = gameInfoService;
        logger.SetModule(LifecycleKeys.ModuleName);
    }
    public async Task<GameInfoEntity?> Handle(GetGameInfoQuery request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                () => _gameInfoService.LoadGameInfoAsync(cancellationToken),
                () => default
                );
}
