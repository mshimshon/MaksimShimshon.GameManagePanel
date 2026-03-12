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
    public async Task CreateAsync()
    {
        if (Create == default) return;
        await Dispatcher.Prepare<CreateModListAction>()
            .With(p => p.Id, Create!.Id)
            .With(p => p.Name, Create.Name)
            .DispatchAsync();
    }

    public async Task GetAsync(Guid id)
    {
        await Dispatcher.Prepare<GetModListAction>()
            .With(p => p.Id, id)
            .DispatchAsync();
    }

    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading;

}
