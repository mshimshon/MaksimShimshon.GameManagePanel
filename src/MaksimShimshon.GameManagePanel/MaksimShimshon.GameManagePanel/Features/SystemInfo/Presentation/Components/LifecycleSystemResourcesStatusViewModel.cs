using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components;

public class LifecycleSystemResourcesStatusViewModel
{
    private bool _loading = false;
    public bool Loading
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

    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public LifecycleSystemResourcesStatusViewModel(IStatePulse statePulse, Configuration configuration)
    {
        _statePulse = statePulse;
        Configuration = configuration;
    }
}
