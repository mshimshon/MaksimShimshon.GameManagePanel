using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

internal class ModListEditorViewModel : WidgetViewModelBase, IModListEditorViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateState);
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    public Dictionary<PartId, List<ModEntity>>? Information { get; set; }
    public Guid Id { get; set; }
    private bool _isProcessing = false;
    private bool _reprocess = false;
    private object _lock = new();
    public ModListEditorViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;

    }
    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading || Information == default;
    public async Task LoadAsync(bool force = false)
    {
        Guid id = Id;
        bool isIdUnknown = id == Guid.Empty && ModListLocalState.Current == default;
        bool isUsingState = id == Guid.Empty && ModListLocalState.Current != default;

        if (isIdUnknown)
            return;
        else if (isUsingState)
            id = ModListLocalState.Current!.Descriptor.Id;

        bool isStateSameAsId = id != Guid.Empty && ModListLocalState.Current != default && ModListLocalState.Current.Descriptor.Id == Id;
        if (isStateSameAsId && !force)
        {
            _ = ProcessModListParts();
            return;
        }

        // TODO: Dispatch

        lock (_lock)
        {
            if (!_reprocess)
                _reprocess = true;
        }

    }

    private async Task ProcessModListParts(CancellationToken ct = default)
    {
        lock (_lock)
        {

            if (_isProcessing) return;
            _isProcessing = true;
            if (_reprocess)
                _reprocess = false;
        }

        if (ModListLocalState.Current == default) return;
        Information = ModListLocalState.Current.Mods
            .ToDictionary(p => p.Key with { }, p => p.Value.Select(p => p with { })
            .ToList());
        await UpdateChanges();
        lock (_lock)
        {
            _isProcessing = false;
        }
    }

    public async Task MoveTo(PartId partId, ModEntity toMove, int targetIndex)
    {
        // TODO: TOAST NOTIFICATION
        if (Information == default) return;

        var list = Information[partId].ToList();
        var fallbackIndex = list.IndexOf(toMove);
        list.Remove(toMove);
        if (targetIndex > list.Count || targetIndex < 0)
            list.Insert(fallbackIndex, toMove);
        else
            list.Insert(targetIndex, toMove);
    }

    private async Task UpdateState()
    {
        bool isReprocessRequired = _reprocess && ModListLocalState.Current != default || Information == default && ModListLocalState.Current != default;
        if (isReprocessRequired)
            _ = ProcessModListParts();

        await UpdateChanges();
    }

    public string GetModName(ModEntity item)
        => item.Name == default ? item.Id.Id : item.Name.Name;
}
