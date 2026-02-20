using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers.Middlewares;

/// <summary>
/// Provides middleware for handling installation state changes within the reducer pipeline. Publishes installation
/// state change events to the event bus after the reducer processes an action.
/// </summary>
internal class SpreadInstallationStateMiddleware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;
    private readonly ICrazyReport _crazyReport;

    public SpreadInstallationStateMiddleware(IEventBus eventBus, ICrazyReport crazyReport)
    {
        _eventBus = eventBus;
        _crazyReport = crazyReport;
    }

    public async Task AfterReducing(object state, object action)
    {
        _crazyReport.ReportInfo("Reducer {0} Called for State {1} with Action {2}", nameof(SpreadInstallationStateMiddleware), state.GetType(), action.GetType());
        if (state.GetType() == typeof(InstallationState))
        {
            _crazyReport.ReportInfo("Reducer {0} Detected State {1} change therefore triggering {2}", nameof(SpreadInstallationStateMiddleware), state.GetType(), LinuxGameServerKeys.Events.OnGameServerInstallStateChanged);
            _ = _eventBus.PublishDataAsync(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged, state);

        }
    }

    public Task BeforeReducing(object state, object action) => Task.CompletedTask;


}