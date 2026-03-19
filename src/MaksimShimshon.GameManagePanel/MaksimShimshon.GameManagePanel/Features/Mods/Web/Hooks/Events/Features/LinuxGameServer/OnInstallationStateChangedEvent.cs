using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.Events.Features.LinuxGameServer.Dto;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Events.Features.LinuxGameServer;

[EventBusId(LinuxGameServerKeys.Events.OnGameServerInstallStateChanged)]
internal class OnInstallationStateChangedEvent : IEventBusHandler
{
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<ModListState> _modListState;
    private readonly ICrazyReport _crazyReport;

    public OnInstallationStateChangedEvent(IDispatcher dispatcher, IStateAccessor<ModListState> modListState, ICrazyReport crazyReport)
    {
        _dispatcher = dispatcher;
        _modListState = modListState;
        _crazyReport = crazyReport;
        crazyReport.SetModule<OnInstallationStateChangedEvent>(ModListKeys.ModuleName);
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        _crazyReport.ReportInfo("Incoming Event from {0}", LinuxGameServerKeys.Events.OnGameServerInstallStateChanged);
        var state = await evt.ReadAs<InstallationStateResponse>();
        _crazyReport.ReportInfo("InstallationStateResponse is {0}", state.ToString());
        if (state.IsInstallationCompleted && !_modListState.State.IsSchematicPartsLoading)
            await _dispatcher.Prepare<LoadModListSchematicAction>().DispatchAsync();
    }
}