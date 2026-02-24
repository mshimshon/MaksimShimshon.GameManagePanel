using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

public class StartupParameterViewModel : WidgetViewModelBase, IStartupParameterViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly ICrazyReport _crazyReport;

    public StartupParameterViewModel(IStatePulse statePulse, ICrazyReport crazyReport)
    {
        _statePulse = statePulse;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<StartupParameterViewModel>(LifecycleModule.ModuleName);
        _ = GroupingParameters();
    }


    public GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, OnUpdate);
    public Dictionary<string, List<GameStartupParameterEntity>> Parameters { get; private set; } = new();

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;

    public Dictionary<string, string> StartupParameters => GameInfoState.StartupParameters;

    public bool SavedParametersLoaded => GameInfoState.SavedParametersLoaded;


    private async Task OnUpdate()
    {

        await GroupingParameters();
        _crazyReport.ReportInfo("GameInfoState has updated now rerendering.");
        foreach (var item in Parameters.Values)
        {
            foreach (var param in item)
            {
                _crazyReport.ReportInfo("Loaded Parameter {0} = {1}", param.Key.Key, GetInitialValue(param));

            }
        }

        foreach (var item in StartupParameters)
        {
            _crazyReport.ReportInfo("Loaded User Defined Parameter {0} = {1}", item.Key, item.Value);
        }

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

    public string GetInitialValue(GameStartupParameterEntity parameter) =>
        StartupParameters.ContainsKey(parameter.Key.Key) ?
        StartupParameters[parameter.Key.Key] :
        parameter.DefaultValue ?? string.Empty;

}
