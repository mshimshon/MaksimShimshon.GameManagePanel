using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public class SetupProcessViewModel : WidgetViewModelBase, ISetupProcessViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;
    public string KeyGame { get; set; } = default!;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateState);
    public string RepositoryTarget { get; }
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    private bool _isInstallCompleted;
    public async Task UpdateState()
    {

        //Console.WriteLine($"State updated: {(InstallState.ToString())}");
        LastUpdate = DateTime.UtcNow;
        if (_isInstallCompleted != InstallState.IsInstallationCompleted)
        {
            _isInstallCompleted = InstallState.IsInstallationCompleted;
            await UpdateParentChanges();
        }
        else
            await UpdateChanges();
        //await Task.Delay(10000);
        //await UpdateChanges();
    }
    public SetupProcessViewModel(IStatePulse statePulse, PluginConfiguration pluginConfiguration, ICrazyReport crazyReport)
    {
        _statePulse = statePulse;
        _dispatcher = statePulse.Dispatcher;
        RepositoryTarget = pluginConfiguration.Repositories.GitGameServerScriptRepository;
        crazyReport.SetModule(LinuxGameServerModule.ModuleName);
        crazyReport.ReportInfo("Loaded Widget {0} and Found {1} Games Available.", nameof(SetupProcessViewModel), InstallState.AvailableGameServers.Count);

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
    protected override bool GetStateLoadingStatus() => !InstallState.IsInstalledGameDiskLoaded || !InstallState.IsProgressDiskLoaded;

    protected override void OnViewModelDispose()
    {
        Console.WriteLine("VM Disposed");
    }
}
