using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.UI.Components.ViewModels;

public interface IWidgetModListWorkspaceViewModel : IWidgetViewModel
{
    ModListLocalState ModListLocalState { get; }

}
