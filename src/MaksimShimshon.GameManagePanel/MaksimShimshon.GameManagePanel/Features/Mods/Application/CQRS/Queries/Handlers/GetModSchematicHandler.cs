using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries.Handlers;

internal sealed class GetModSchematicHandler : HandlerBase, IRequestHandler<GetModSchematicQuery, IReadOnlyCollection<PartSchematicEntity>?>
{
    private readonly IModListService _modListService;

    public GetModSchematicHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModSchematicHandler> logger) : base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.ModuleName);
    }

    public async Task<IReadOnlyCollection<PartSchematicEntity>?> Handle(GetModSchematicQuery request, CancellationToken cancellationToken)
            => await ExecAndHandleExceptions(
                () => _modListService.GetSchematic(cancellationToken),
                () => default
                );
}
