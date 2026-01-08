using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components.ViewModels;

public class WidgetStartupParametersViewModel : WidgetViewModelBase, IWidgetStartupParametersViewModel
{

    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    private LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, UpdateChanges);

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;


    public WidgetStartupParametersViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

}
