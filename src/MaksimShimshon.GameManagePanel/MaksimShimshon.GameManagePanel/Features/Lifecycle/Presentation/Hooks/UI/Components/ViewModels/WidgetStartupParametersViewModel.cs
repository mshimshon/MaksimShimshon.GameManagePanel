using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components.ViewModels;

public class WidgetStartupParametersViewModel : IWidgetStartupParametersViewModel
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
    private readonly IDispatcher _dispatcher;

    private LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, OnUpdate);

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;


    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public WidgetStartupParametersViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

}
