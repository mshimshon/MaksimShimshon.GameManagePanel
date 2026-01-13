using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers.Middlewares;

internal class InstallationStateReducerMiddleware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;

    public InstallationStateReducerMiddleware(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task AfterReducing(object state, object action)
    {
        if (state.GetType() == typeof(InstallationState))
            await _eventBus.PublishDataAsync(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged, state);
    }

    public Task BeforeReducing(object state, object action) => Task.CompletedTask;

}