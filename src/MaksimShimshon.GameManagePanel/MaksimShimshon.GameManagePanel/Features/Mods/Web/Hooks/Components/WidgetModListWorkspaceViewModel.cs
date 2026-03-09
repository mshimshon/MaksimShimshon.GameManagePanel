using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components.ViewModels;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components;

internal class WidgetModListWorkspaceViewModel : WidgetViewModelBase, IWidgetModListWorkspaceViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateChanges);
    public Guid ModListId { get; set; }
    public WidgetModListWorkspaceViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }


    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading;

    public Guid GetModListId()
    {
        if (ModListLocalState.Current == default)
            return ModListId;
        return ModListLocalState.Current.Descriptor.Id;
    }
}
