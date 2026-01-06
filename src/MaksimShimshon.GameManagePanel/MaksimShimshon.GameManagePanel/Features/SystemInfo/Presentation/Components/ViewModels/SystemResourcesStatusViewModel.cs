using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components.ViewModels;

public class SystemResourcesStatusViewModel : ISystemResourcesStatusViewModel
{
    private bool _loading = false;
    public bool IsLoading
    {
        get => _loading;
        private set
        {
            bool hasChanged = value != _loading;
            _loading = value;
            if (hasChanged)
                _ = SpreadChanges?.Invoke();
        }
    }
    public event Func<Task>? SpreadChanges;

    private readonly IStatePulse _statePulse;


    public SystemInfoState SystemState => _statePulse.StateOf<SystemInfoState>(() => this, OnUpdate);
    public Configuration Configuration { get; }

    public SystemInfoEntity? SystemInfo => SystemState.SystemInfo;
    public DateTime LastUpdate => SystemState.LastUpdate;
    public int Delay => SystemState.Delay;

    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public SystemResourcesStatusViewModel(IStatePulse statePulse, Configuration configuration)
    {
        _statePulse = statePulse;
        Configuration = configuration;
    }
}
