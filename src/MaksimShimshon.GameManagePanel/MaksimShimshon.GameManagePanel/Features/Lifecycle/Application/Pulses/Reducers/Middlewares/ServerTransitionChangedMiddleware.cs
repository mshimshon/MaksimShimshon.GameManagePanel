using GameServerManager.Core.Abstractions.Plugin;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Events;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores.Enums;
using StatePulse.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers.Middlewares;

internal class ServerTransitionChangedMiddleware : IReducerMiddleware
{
    private readonly IPluginEventBus _eventBus;

    public ServerTransitionChangedMiddleware(IPluginEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public Task AfterReducing(object state, object action) => Task.CompletedTask;
    public async Task BeforeReducing(object state, object action)
    {
        bool isCorrectTargetAction = action is LifecycleServerStatusTransitionDoneAction;
        bool isCorrectTargetState = state is LifecycleServerState;
        if (!isCorrectTargetAction || !isCorrectTargetState) return;

        LifecycleServerState serverState = (LifecycleServerState)state;
        if (serverState.Transition == ServerTransition.Starting)
        {
            if (serverState.ServerInfo == default || serverState.ServerInfo.Status != Domain.Enums.Status.Running)
                await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerStartFailed));
            else if (serverState.ServerInfo != default && serverState.ServerInfo.Status == Domain.Enums.Status.Running)
                await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerStartSuccess));

            await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerStopFinish));
        }
        else if (serverState.Transition == ServerTransition.Stopping)
        {
            if (serverState.ServerInfo == default || serverState.ServerInfo.Status != Domain.Enums.Status.Stopped)
                await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerStopFailed));
            else if (serverState.ServerInfo != default && serverState.ServerInfo.Status == Domain.Enums.Status.Stopped)
                await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerStopSuccess));

            await _eventBus.PublishAsync(new LifecycleEventMessageNoData(LifecycleEvents.ServerStopFinish));
        }
    }

}
