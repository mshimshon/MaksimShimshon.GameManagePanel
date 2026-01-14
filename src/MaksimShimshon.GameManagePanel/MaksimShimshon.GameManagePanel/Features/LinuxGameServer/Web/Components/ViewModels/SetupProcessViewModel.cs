using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public class SetupProcessViewModel : WidgetViewModelBase, ISetupProcessViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;


    public string KeyGame { get; set; } = default!;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateChanges);

    public SetupProcessViewModel(IStatePulse statePulse, IMedihater medihater, PluginConfiguration pluginConfiguration)
    {
        _statePulse = statePulse;
        _dispatcher = statePulse.Dispatcher;
    }

    public async Task InstallAsync()
    {
        IsLoading = true;
        await _dispatcher.Prepare<InstallGameServerAction>()
            .With(p => p.Id, KeyGame)
            .With(p => p.DisplayName, InstallState.AvailableGameServers[KeyGame])
            .DispatchAsync();
        IsLoading = false;

    }

}
