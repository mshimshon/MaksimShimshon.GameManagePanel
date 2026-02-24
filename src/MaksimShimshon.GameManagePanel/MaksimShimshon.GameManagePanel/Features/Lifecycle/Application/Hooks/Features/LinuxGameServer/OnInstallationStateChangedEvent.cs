using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Hooks.Features.LinuxGameServer.Dto;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Hooks.Features.LinuxGameServer;

[EventBusId(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged)]
internal class OnInstallationStateChangedEvent : IEventBusHandler
{
    /*
     * IScheduledEventBusHandler
     * Receiver Receives -> Event -> Fetch All Match Id if cross circuit resolve for all circuits call 
     * Check if type Implement IEventBusHandler.HandleAsync or IScheduledEventBusHandler
     */
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<GameInfoState> _gameInfoStateAccess;
    private readonly ICrazyReport _crazyReport;

    public OnInstallationStateChangedEvent(IDispatcher dispatcher, IStateAccessor<GameInfoState> gameInfoStateAccess, ICrazyReport crazyReport)
    {
        _dispatcher = dispatcher;
        _gameInfoStateAccess = gameInfoStateAccess;
        _crazyReport = crazyReport;
        crazyReport.SetModule<OnInstallationStateChangedEvent>(LifecycleModule.ModuleName);
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        _crazyReport.ReportInfo("Incoming Event from {0}", LinuxGameServerKeys.Events.OnGameServerInstallStateChanged);
        _crazyReport.ReportInfo("GameInfo is {0}", _gameInfoStateAccess.State.GameInfo?.ToString() ?? "null");
        if (_gameInfoStateAccess.State.GameInfo != default) return;
        var state = await evt.ReadAs<InstallationStateResponse>();
        _crazyReport.ReportInfo("InstallationStateResponse is {0}", state.ToString());
        if (state.IsInstallationCompleted)
            await _dispatcher.Prepare<ServerGameInfoUpdateAction>().DispatchAsync();
    }
}
