using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components;

public class WidgetStartupParametersViewModel
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
    private readonly IDispatcher _dispatcher;

    public LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, OnUpdate);
    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public WidgetStartupParametersViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

}
