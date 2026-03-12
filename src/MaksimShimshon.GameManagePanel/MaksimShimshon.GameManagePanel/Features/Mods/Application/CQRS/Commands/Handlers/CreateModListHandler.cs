using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MedihatR;
using Microsoft.Extensions.Logging;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands.Handlers;

internal class CreateModListHandler : HandlerBase, IRequestHandler<CreateModListCommand, ModListEntity?>
{
    private readonly IModListService _modListService;

    public CreateModListHandler(IModListService modListService, INotificationService notificationService, ILogger<CreateModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
    }

    public async Task<ModListEntity?> Handle(CreateModListCommand request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                () => _modListService.CreateAsync(new(request.Id, request.Name)),
                () => default
                );
}
