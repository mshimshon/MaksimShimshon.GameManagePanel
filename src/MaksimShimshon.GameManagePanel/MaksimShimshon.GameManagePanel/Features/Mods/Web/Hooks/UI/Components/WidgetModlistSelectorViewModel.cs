using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.UI.Components.ViewModels;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.UI.Components;

internal class WidgetModlistSelectorViewModel : WidgetViewModelBase, IWidgetModlistSelectorViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListState ModsListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);

    public WidgetModlistSelectorViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }
}
