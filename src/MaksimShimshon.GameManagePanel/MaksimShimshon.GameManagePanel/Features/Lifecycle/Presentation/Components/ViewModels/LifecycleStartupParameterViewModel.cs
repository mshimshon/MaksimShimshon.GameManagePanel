using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;

public class LifecycleStartupParameterViewModel : ILifecycleStartupParameterViewModel
{
    private readonly IStatePulse _statePulse;
    public LifecycleStartupParameterViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
        _ = GroupingParameters();
    }

    private bool _loading = false;
    public bool IsLoading
    {
        get => _loading;
        private set
        {
            bool hasChanged = value != _loading;
            _loading = value;
            if (hasChanged)
                _ = OnUpdate();
        }
    }
    public event Func<Task>? SpreadChanges;


    public LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, OnUpdate);
    public Dictionary<string, List<GameStartupParameterEntity>> Parameters { get; private set; } = new();

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;

    public Dictionary<string, string> StartupParameters => GameInfoState.StartupParameters;

    public bool SavedParametersLoaded => GameInfoState.SavedParametersLoaded;


    private async Task OnUpdate()
    {
        await GroupingParameters();
        _ = SpreadChanges?.Invoke();
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
