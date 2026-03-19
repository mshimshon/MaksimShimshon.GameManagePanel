using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands.Handlers;

internal class CreateModListHandler : HandlerBase, IRequestHandler<CreateModListCommand>
{
    private readonly IModListService _modListService;

    public CreateModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<CreateModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.ModuleName);
    }

    public async Task Handle(CreateModListCommand request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                () => _modListService.CreateAsync(new(request.Id, request.Name)),
                ex => throw ex
                );
}
