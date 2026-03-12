using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

internal sealed class ModListEditorViewModel : WidgetViewModelBase, IModListEditorViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateState);
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    public Dictionary<PartId, List<ModEntity>>? Information { get; set; }
    public Guid InitialId { get; set; }

    private bool _isProcessing = false;
    private bool _reprocess = false;
    private object _lock = new();
    public ModListEditorViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;

    }
    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading || Information == default;

    protected override async Task OnViewModelParametersSetAsync()
    {
        if (ModListLocalState.Current == default)
        {
            Information = default;
            InitialId = Guid.Empty;
            return;
        }

        bool informationUndefined = Information == default;
        bool informationDifferent = ModListLocalState.Current.Descriptor.Id != InitialId;
        bool isReprocessRequired = _reprocess || informationUndefined || informationDifferent;
        if (isReprocessRequired)
            await ProcessModListParts();

        InitialId = ModListLocalState.Current?.Descriptor.Id ?? Guid.Empty;
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
        try
        {
            Information = default;
            if (ModListLocalState.Current == default) return;
            Information = ModListLocalState.Current.Mods
                .ToDictionary(p => p.Key, p => p.Value.ToList());
        }
        catch (Exception)
        {
            // TODO: LOGG
        }
        finally
        {
            lock (_lock)
            {
                _isProcessing = false;
            }
            await UpdateChanges();
        }
    }

    public void RemoveFrom(string partId, ModEntity toRemove)
    {
        // TODO: TOAST NOTIFICATION
        if (Information == default || !Information.Any(p => p.Key.Id == partId))
            return;
        var kp = Information.Single(p => p.Key.Id == partId);
        kp.Value.Remove(toRemove);
        _ = UpdateChanges();
    }

    public void AddTo(string partId, ModEntity toAdd)
    {
        // TODO: TOAST NOTIFICATION
        if (Information == default || !Information.Any(p => p.Key.Id == partId))
            return;
        var kp = Information.Single(p => p.Key.Id == partId);
        kp.Value.Add(toAdd);
        _ = UpdateChanges();
    }

    public void MoveTo(string partId, ModEntity toMove, int targetIndex)
    {
        // TODO: TOAST NOTIFICATION
        if (Information == default || !Information.Any(p => p.Key.Id == partId))
            return;

        var kp = Information.Single(p => p.Key.Id == partId);
        var list = kp.Value;
        var fallbackIndex = list.IndexOf(toMove);
        list.Remove(toMove);
        if (targetIndex > list.Count || targetIndex < 0)
            list.Insert(fallbackIndex, toMove);
        else
            list.Insert(targetIndex, toMove);
        Information[kp.Key] = list;

        _ = UpdateChanges();
    }

    public async Task SaveAsync()
    {
        // TODO: SEND SP ACTION
    }

    public async Task CloseAsync()
    {

    }

    private async Task UpdateState()
    {
        await UpdateChanges();
    }

    public string GetModName(ModEntity item)
        => item.Name == default ? item.Id.Id : item.Name.Name;


}
