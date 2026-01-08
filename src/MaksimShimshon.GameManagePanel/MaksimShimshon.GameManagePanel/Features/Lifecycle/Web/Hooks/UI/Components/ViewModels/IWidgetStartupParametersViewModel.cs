using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.UI.Components.ViewModels;

public interface IWidgetStartupParametersViewModel : IWidgetViewModel
{
    public GameInfoEntity? GameInfo { get; }
}
