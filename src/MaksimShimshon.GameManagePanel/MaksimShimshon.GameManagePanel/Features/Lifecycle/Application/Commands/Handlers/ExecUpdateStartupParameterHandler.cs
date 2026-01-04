using GameServerManager.Core.Abstractions.Exceptions;
using GameServerManager.Core.Abstractions.Plugin;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Events;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MedihatR;
using StatePulse.Net;
namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Commands.Handlers;

public class ExecUpdateStartupParameterHandler : IRequestHandler<ExecUpdateStartupParameterCommand>
{
    private readonly ILifecycleServices _lifecycleServices;
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<LifecycleGameInfoState> _gameinfoStateAccessor;
    private readonly IPluginEventBus _eventBus;

    public ExecUpdateStartupParameterHandler(ILifecycleServices lifecycleServices, IDispatcher dispatcher, IStateAccessor<LifecycleGameInfoState> gameinfoStateAccessor, IPluginEventBus eventBus)
    {
        _lifecycleServices = lifecycleServices;
        _dispatcher = dispatcher;
        _gameinfoStateAccessor = gameinfoStateAccessor;
        _eventBus = eventBus;
    }

    public async Task Handle(ExecUpdateStartupParameterCommand request, CancellationToken cancellationToken) 
       {
        try
        {
            await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.UpdateStartupParametersBegin));
            await _lifecycleServices.UpdateStartupParameterAsync(request.Key, request.Value, cancellationToken);

            if (_gameinfoStateAccessor.State.StartupParameters.ContainsKey(request.Key))
                _gameinfoStateAccessor.State.StartupParameters[request.Key] = request.Value;
            else
                _gameinfoStateAccessor.State.StartupParameters.Add(request.Key, request.Value);

            await _dispatcher.Prepare<LifecycleServerGameInfoUpdatedAction>()
                .With(p => p.GameInfo, _gameinfoStateAccessor.State.GameInfo)
                .DispatchAsync();
        }
        catch (WebServiceException ex)
        {
            await _dispatcher.Prepare<SendToastNotificationAction>()
                .With(p => p.Message, ex.Message)
                .With(p => p.Color, ToastColor.Error)
                .DispatchAsync();
            await _eventBus.PublishAsync(new LifecycleEventMessage(LifecycleEvents.UpdateStartupParametersFailed, ex));
        }
        catch(Exception ex)
        {
            await _dispatcher.Prepare<SendToastNotificationAction>()
                .With(p => p.Message, "Unknown Error, Please contact admins if persistent.")
                .With(p => p.Color, ToastColor.Error)
                .DispatchAsync();
            await _eventBus.PublishAsync(new LifecycleEventMessage(LifecycleEvents.UpdateStartupParametersFailed, ex));
        }
    }
}
