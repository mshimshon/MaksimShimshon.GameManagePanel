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
    public Func<Task> SpreadChanges { get; set; } = default!;

    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;


    public LifecycleSystemState SystemState => _statePulse.StateOf<LifecycleSystemState>(() => this, OnUpdate);
    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public LifecycleSystemResourcesStatusViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;

    }
}
