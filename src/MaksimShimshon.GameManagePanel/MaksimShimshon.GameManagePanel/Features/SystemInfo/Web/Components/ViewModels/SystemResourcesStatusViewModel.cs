using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components.ViewModels;

public class SystemResourcesStatusViewModel : WidgetViewModelBase, ISystemResourcesStatusViewModel
{

    private readonly IStatePulse _statePulse;

    public SystemInfoState SystemState => _statePulse.StateOf<SystemInfoState>(() => this, UpdateState);
    public PluginConfiguration Configuration { get; }

    public SystemInfoEntity? SystemInfo => SystemState.SystemInfo;
    public DateTime LastUpdate => SystemState.LastUpdate;
    public SystemResourcesStatusViewModel(IStatePulse statePulse, PluginConfiguration configuration)
    {
        _statePulse = statePulse;
        Configuration = configuration;
    }
    public async Task UpdateState()
    {
        await UpdateChanges();
    }

}
