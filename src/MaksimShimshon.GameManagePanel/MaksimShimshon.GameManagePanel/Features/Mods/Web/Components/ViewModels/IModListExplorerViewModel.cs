using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;

public interface IModListExplorerViewModel : IWidgetViewModel
{
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    Task GetAsync(Guid id);
    Task GetAvailableAsync();
}
