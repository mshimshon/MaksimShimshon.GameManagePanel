using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Api;
using MedihatR;
using Microsoft.Extensions.Logging;
namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;

public class ExecUpdateStartupParameterHandler : HandlerBase, IRequestHandler<ExecUpdateStartupParameterCommand>
{
    private readonly ILifecycleServices _lifecycleServices;

    public ExecUpdateStartupParameterHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ILogger<ExecUpdateStartupParameterHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
    }
    public async Task Handle(ExecUpdateStartupParameterCommand request, CancellationToken cancellationToken)
    {
        await ExecAndHandleExceptions(() => _lifecycleServices.UpdateStartupParameterAsync(request.Key, request.Value, cancellationToken));
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
