using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

public class LifecycleStartupParameterViewModel : WidgetViewModelBase, ILifecycleStartupParameterViewModel
{
    private readonly IStatePulse _statePulse;
    public LifecycleStartupParameterViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
        _ = GroupingParameters();
    }


    public LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, OnUpdate);
    public Dictionary<string, List<GameStartupParameterEntity>> Parameters { get; private set; } = new();

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;

    public Dictionary<string, string> StartupParameters => GameInfoState.StartupParameters;

    public bool SavedParametersLoaded => GameInfoState.SavedParametersLoaded;


    private async Task OnUpdate()
    {
        await GroupingParameters();
        _ = UpdateChanges();
    }

    public Task GroupingParameters()
    {
        Parameters = GameInfoState.GameInfo?.StartupParameters != default ? GameInfoState.GameInfo.StartupParameters
            .Where(p => !string.IsNullOrEmpty(p.Category))
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.ToList())
            : new();
        return Task.CompletedTask;
    }


}
