using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;

public interface IModListSelectorViewModel : IWidgetViewModel
{
    string? CurrentModList { get; set; }
    ModListState ModListState { get; }

    Task Save();

}
