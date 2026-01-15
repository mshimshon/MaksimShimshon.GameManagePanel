using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers.Middlewares;

/// <summary>
/// Provides middleware for handling installation state changes within the reducer pipeline. Publishes installation
/// state change events to the event bus after the reducer processes an action.
/// </summary>
internal class SpreadInstallationStateMiddleware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;

    public SpreadInstallationStateMiddleware(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task AfterReducing(object state, object action)
    {
        if (action.GetType() == typeof(ReceivingUpdatedInstallStateAction)) return;

        if (state.GetType() == typeof(InstallationState))
            await _eventBus.PublishDataAsync(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged, state);
    }

    public Task BeforeReducing(object state, object action) => Task.CompletedTask;

}