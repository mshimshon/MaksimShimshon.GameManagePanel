using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;
namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecUpdateStartupParameterHandler : HandlerBase, IRequestHandler<ExecUpdateStartupParameterCommand>
{
    private readonly IStartupParameterService _startupParameterService;

    public ExecUpdateStartupParameterHandler(IStartupParameterService startupParameterService, INotificationService notificationService, ICrazyReport<ExecUpdateStartupParameterHandler> logger) : base(notificationService, logger)
    {
        _startupParameterService = startupParameterService;
        logger.SetModule(LifecycleKeys.ModuleName);
    }
    public async Task Handle(ExecUpdateStartupParameterCommand request, CancellationToken cancellationToken)
    {
        await ExecAndHandleExceptions(() => _startupParameterService.UpdateStartupParameterAsync(request.Key, request.Value, cancellationToken));
        //TODO: Implement the following inside the service level
        //if (_gameinfoStateAccessor.State.StartupParameters.ContainsKey(request.Key))
        //    _gameinfoStateAccessor.State.StartupParameters[request.Key] = request.Value;
        //else
        //    _gameinfoStateAccessor.State.StartupParameters.Add(request.Key, request.Value);

        //await _dispatcher.Prepare<LifecycleServerGameInfoUpdatedAction>()
        //    .With(p => p.GameInfo, _gameinfoStateAccessor.State.GameInfo)
        //    .DispatchAsync();
    }
}
