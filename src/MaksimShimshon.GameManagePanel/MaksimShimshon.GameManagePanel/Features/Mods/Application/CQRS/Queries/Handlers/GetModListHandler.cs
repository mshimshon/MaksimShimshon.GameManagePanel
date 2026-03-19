using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries.Handlers;

internal class GetModListHandler : HandlerBase, IRequestHandler<GetModListQuery, ModListEntity?>
{
    private readonly IModListService _modListService;

    public GetModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.ModuleName);
    }
    public async Task<ModListEntity?> Handle(GetModListQuery request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                () => _modListService.GetAsync(request.Id, cancellationToken),
                () => default
                );

}
