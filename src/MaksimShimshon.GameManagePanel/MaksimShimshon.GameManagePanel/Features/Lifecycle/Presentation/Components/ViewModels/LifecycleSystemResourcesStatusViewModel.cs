using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;

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


    public LifecycleSystemState SystemState => _statePulse.StateOf<LifecycleSystemState>(() => this, OnUpdate);
    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public LifecycleSystemResourcesStatusViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }
}
