using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers.Middlewares;

internal class GameServerInstallEventMiddlware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;
    public GameServerInstallEventMiddlware(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task AfterReducing(object state, object action)
    {
        if (state.GetType() != typeof(InstallGameServerAction))
            return;

        var actionConverted = (InstallGameServerAction)action;
        var eventInstallStarted = _eventBus.PublishDataAsync(LinuxGameServerKeys.Events.OnGameServerInstall, actionConverted.Id);
        if (!eventInstallStarted.IsCompletedSuccessfully)
            await eventInstallStarted;
    }

    public Task BeforeReducing(object state, object action)
        => Task.CompletedTask;

}
