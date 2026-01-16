using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components.ViewModels;

public class SystemResourcesStatusViewModel : WidgetViewModelBase, ISystemResourcesStatusViewModel
{

    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public SystemInfoState SystemState => _statePulse.StateOf<SystemInfoState>(() => this, UpdateState);
    public PluginConfiguration Configuration { get; }

    public SystemInfoEntity? SystemInfo => SystemState.SystemInfo;
    public DateTime LastUpdate => SystemState.LastUpdate;
    public int Delay => SystemState.Delay;

    public SystemResourcesStatusViewModel(IStatePulse statePulse, PluginConfiguration configuration, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        Configuration = configuration;
        _dispatcher = dispatcher;
    }
    private bool AutoUpdateActivated { get; set; }
    private bool IsUpdating { get; set; }
    private DateTime LastLaunch { get; set; }
    public async Task UpdateState()
    {
        if (SystemState.LastUpdate >= LastLaunch)
            IsUpdating = false;
        await UpdateChanges();
    }

    public async Task StartPeriodicUpdates()
    {
        if (AutoUpdateActivated) return;
        AutoUpdateActivated = true;
        do
        {
            if (IsUpdating) continue;
            await FetchUpdatedSystemInfo();
            TimeSpan delay = new(0, 0, SystemState.Delay);
            await Task.Delay(delay);
        } while (AutoUpdateActivated);
    }

    private async Task FetchUpdatedSystemInfo()
    {
        LastLaunch = DateTime.UtcNow;
        IsUpdating = true;
        await _dispatcher.Prepare<SystemInfoUpdateAction>().DispatchAsync();
    }
}
