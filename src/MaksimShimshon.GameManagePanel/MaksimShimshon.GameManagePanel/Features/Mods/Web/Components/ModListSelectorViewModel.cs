using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

internal sealed class ModListSelectorViewModel : WidgetViewModelBase, IModListSelectorViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly ICrazyReport _crazyReport;

    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateState);
    public Guid CurrentModList { get; set; }
    public Guid InitialValue { get; private set; }
    public ModListSelectorViewModel(IStatePulse statePulse, ICrazyReport crazyReport)
    {
        _statePulse = statePulse;
        _crazyReport = crazyReport;
        InitialValue = ModListState.Active?.Descriptor.Id ?? Guid.Empty;
        CurrentModList = InitialValue;
        _crazyReport.SetModule<ModListSelectorViewModel>(ModKeys.ModuleName);
    }
    public async Task UpdateState()
    {
        bool areBothDefined = InitialValue != Guid.Empty && ModListState.Active != default;
        bool wasCurrentSetWhenInitialDef = InitialValue == Guid.Empty && ModListState.Active != default;
        bool wasInitialSetWhenCurrentDef = InitialValue != Guid.Empty && ModListState.Active == default;
        bool bothdefinedButDifferent = areBothDefined && InitialValue != ModListState.Active!.Descriptor.Id;

        bool requiresUpdate = wasCurrentSetWhenInitialDef || wasInitialSetWhenCurrentDef || bothdefinedButDifferent;
        if (requiresUpdate)
        {

            InitialValue = ModListState.Active?.Descriptor.Id ?? Guid.Empty;
            CurrentModList = InitialValue;
        }


        await UpdateChanges();

    }
    public async Task Save()
    {
        ModListEntity? newElement = CurrentModList != default ? ModListState.Available.SingleOrDefault(p => p.Descriptor.Id == CurrentModList) : default;
        await _statePulse.Dispatcher
            .Prepare<UpdateCurrentModListAction>()
            .With(p => p.Current, newElement)
            .DispatchAsync();
    }
}
