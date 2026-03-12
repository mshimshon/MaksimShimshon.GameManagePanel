using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries.Handlers;

internal class GetModListHandler : HandlerBase, IRequestHandler<GetModListQuery, ModListEntity?>
{
    private readonly IModListService _modListService;

    public GetModListHandler(IModListService modListService, INotificationService notificationService, ILogger<GetModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
    }
    public async Task<ModListEntity?> Handle(GetModListQuery request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                () => _modListService.GetAsync(request.Id, cancellationToken),
                () => default
                );

}
