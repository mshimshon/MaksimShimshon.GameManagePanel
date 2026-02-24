using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

public interface IStartupParameterViewModel : IWidgetViewModel
{
    public GameInfoEntity? GameInfo { get; }
    public Dictionary<string, string> StartupParameters { get; }
    public bool SavedParametersLoaded { get; }
    Task GroupingParameters();
    Dictionary<string, List<GameStartupParameterEntity>> Parameters { get; }
    string GetInitialValue(GameStartupParameterEntity parameter);
}

