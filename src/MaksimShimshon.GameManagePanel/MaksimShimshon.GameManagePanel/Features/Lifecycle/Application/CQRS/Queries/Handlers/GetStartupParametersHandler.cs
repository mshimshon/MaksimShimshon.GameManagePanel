using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;

public class GetStartupParametersHandler : HandlerBase, IRequestHandler<GetStartupParametersQuery, Dictionary<string, string>>
{
    private readonly IStartupParameterService _startupParameterService;

    public GetStartupParametersHandler(IStartupParameterService startupParameterService, INotificationService notificationService, ICrazyReport<GetStartupParametersHandler> logger) : base(notificationService, logger)
    {
        _startupParameterService = startupParameterService;
        logger.SetModule(LifecycleKeys.ModuleName);
    }
    public async Task<Dictionary<string, string>> Handle(GetStartupParametersQuery request, CancellationToken cancellationToken)
    {
        return await ExecAndHandleExceptions(
            () => _startupParameterService.GetServerStartupParametersAsync(cancellationToken),
            () => new()
            );
    }
}
