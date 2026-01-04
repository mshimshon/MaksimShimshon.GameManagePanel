using GameServerManager.Core.Abstractions.Exceptions;
using GameServerManager.Core.Abstractions.Plugin;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Events;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Notification.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Notification.Enums;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Commands.Handlers;

public class ExecRestartServerHandler : IRequestHandler<ExecRestartServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;
    private readonly IDispatcher _dispatcher;
    private readonly IPluginEventBus _eventBus;

    public ExecRestartServerHandler(ILifecycleServices lifecycleServices, IDispatcher dispatcher, IPluginEventBus eventBus)
    {
        _lifecycleServices = lifecycleServices;
        _dispatcher = dispatcher;
        _eventBus = eventBus;
    }
    public async Task Handle(ExecRestartServerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerRestartBegin));
            await _lifecycleServices.ServerRestartAsync();
        }
        catch (WebServiceException ex)
        {
            await _dispatcher.Prepare<SendToastNotificationAction>()
                .With(p => p.Message, ex.Message)
                .With(p => p.Color, ToastColor.Error)
                .DispatchAsync();
            await _eventBus.PublishAsync(new LifecycleEventMessage(LifecycleEvents.ServerRestartFailed, ex));

        }
        catch (Exception ex)
        {
            await _dispatcher.Prepare<SendToastNotificationAction>()
                .With(p => p.Message, "Unknown Error, Please contact admins if persistent.")
                .With(p => p.Color, ToastColor.Error)
                .DispatchAsync();
            await _eventBus.PublishAsync(new LifecycleEventMessage(LifecycleEvents.ServerRestartFailed, ex));
        }

    }
}
