using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using MaksimShimshon.GameManagePanel.Core;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Hooks.Events.OnBeforeRuntimeInitialization;

[EventBusId(PluginKeys.Events.OnBeforeRuntimeInitialization)]
internal class LoadAvailableGameServerToInstallHandler : IEventBusHandler
{
    private readonly ILinuxGameServerService _linuxGameServerService;
    private readonly IDispatcher _dispatcher;
    private readonly ICrazyReport _crazyReport;

    public LoadAvailableGameServerToInstallHandler(ILinuxGameServerService linuxGameServerService, IDispatcher dispatcher, ICrazyReport crazyReport)
    {
        _linuxGameServerService = linuxGameServerService;
        _dispatcher = dispatcher;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(LinuxGameServerModule.ModuleName);
    }

    public async Task HandleAsync(IEventBusMessage evt)
    {
        var result = await _linuxGameServerService.GetAvailableGames();
        _crazyReport.ReportInfo("Loaded Available Games ({0} Found)", result.Count);
        await _dispatcher.Prepare<PopulateAvailableGamesForInstallAction>(result).DispatchAsync();
    }
}
