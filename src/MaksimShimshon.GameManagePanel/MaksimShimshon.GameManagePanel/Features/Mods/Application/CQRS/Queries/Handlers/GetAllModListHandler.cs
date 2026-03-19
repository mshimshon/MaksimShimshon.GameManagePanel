using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries.Handlers;

internal class GetAllModListHandler : HandlerBase, IRequestHandler<GetAllModListQuery, ICollection<ModListDescriptor>>
{
    private readonly IModListService _modListService;

    public GetAllModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetAllModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.ModuleName);
    }

    public async Task<ICollection<ModListDescriptor>> Handle(GetAllModListQuery request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                () => _modListService.GetAllAsync(cancellationToken),
                () => new List<ModListDescriptor>()
                );
}
