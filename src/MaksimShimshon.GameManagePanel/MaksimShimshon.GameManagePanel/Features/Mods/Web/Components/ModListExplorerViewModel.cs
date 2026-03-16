using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

internal sealed class ModListExplorerViewModel : WidgetViewModelBase, IModListExplorerViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateChanges);
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    private IDispatcher Dispatcher => _statePulse.Dispatcher;
    public ModListDescriptor? Create { get; set; }
    public ModListExplorerViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }

    protected override async Task OnViewModelAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await GetAvailableAsync();
    }
    public async Task GetAvailableAsync()
    {
        IsLoading = true;
        await Dispatcher.Prepare<GetAvailableModListAction>().DispatchAsync();
        IsLoading = false;
    }

    public async Task GetAsync(Guid id)
    {
        IsLoading = true;
        await Dispatcher.Prepare<GetModListAction>()
            .With(p => p.Id, id)
            .DispatchAsync();
        IsLoading = false;
    }

    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading || ModListState.IsLoadingAvailable || !FirstRenderCompleted;

}
