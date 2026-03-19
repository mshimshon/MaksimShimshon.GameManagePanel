using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;

internal class GetRawGameInfoHandler : HandlerBase, IRequestHandler<GetRawGameInfoQuery, string?>
{
    private readonly IGameInfoService _gameInfoReader;

    public GetRawGameInfoHandler(IGameInfoService gameInfoReader, INotificationService notificationService, ICrazyReport<GetRawGameInfoHandler> logger) : base(notificationService, logger)
    {
        _gameInfoReader = gameInfoReader;
        logger.SetModule(LifecycleKeys.ModuleName);
    }

    public async Task<string?> Handle(GetRawGameInfoQuery request, CancellationToken cancellationToken)
    {
        return
            await ExecAndHandleExceptions(
                () => _gameInfoReader.GetRawGameInfoAsync(cancellationToken),
                () => default
                );

    }
}
