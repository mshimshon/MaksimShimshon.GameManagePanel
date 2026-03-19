using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;
using StatePulse.Net;
using System.Diagnostics.CodeAnalysis;

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
    protected override bool GetStateLoadingStatus() => ModListState.IsSchematicPartsLoading || ModListLocalState.IsCurrentLoading || Information == default;

    protected override async Task OnViewModelParametersSetAsync()
    {

    }
    protected override async Task OnViewModelBeforeRenderAsync()
    {
        if (ModListLocalState.Current == default || ModListState.SchematicParts.Count <= 0)
        {
            Information = default;
            InitialId = Guid.Empty;
            return;
        }

        bool informationUndefined = Information == default;
        bool informationDifferent = ModListLocalState.Current.Descriptor.Id != InitialId;
        bool isReprocessRequired = _reprocess || informationUndefined || informationDifferent;
        Console.WriteLine($"ModListEditorViewModel::{isReprocessRequired}");
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
            var information = ModListLocalState.Current.Mods;
            /* 1 -> Check if Current Key Still Exist in Schematic or remove
             * 2 -> add all missing schematic keys into dictionnary
             * 
             */
            var currentIds = information.Keys.Select(k => k).ToHashSet();
            var schematicIds = ModListState.SchematicParts.Select(p => p.Id).ToHashSet();
            bool hasMismatch = !currentIds.SetEquals(schematicIds);

            if (hasMismatch)
            {
                var keepers = information
                    .Where(kvp => schematicIds.Contains(kvp.Key))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());

                foreach (var id in schematicIds)
                {
                    if (!keepers.ContainsKey(id))
                        keepers[id] = new List<ModEntity>();
                }
                Information = keepers;

            }
            else
            {
                Information = information
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());

            }
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

    public void RemoveFrom(PartId partId, ModEntity toRemove)
    {
        // TODO: TOAST NOTIFICATION
        if (!TryGetInInformation(partId, out KeyValuePair<PartId, List<ModEntity>>? kp))
            return;
        kp!.Value.Value.Remove(toRemove);
        _ = UpdateChanges();
    }

    public void AddTo(PartId partId, ModEntity toAdd)
    {
        // TODO: TOAST NOTIFICATION
        if (!TryGetInInformation(partId, out KeyValuePair<PartId, List<ModEntity>>? kp))
            return;
        kp!.Value.Value.Add(toAdd);
        _ = UpdateChanges();
    }

    public void MoveTo(PartId partId, ModEntity toMove, int targetIndex)
    {
        // TODO: TOAST NOTIFICATION
        if (!TryGetInInformation(partId, out KeyValuePair<PartId, List<ModEntity>>? kp))
            return;
        var list = kp!.Value.Value;
        var fallbackIndex = list.IndexOf(toMove);
        list.Remove(toMove);
        if (targetIndex > list.Count || targetIndex < 0)
            list.Insert(fallbackIndex, toMove);
        else
            list.Insert(targetIndex, toMove);
        Information![kp.Value.Key] = list;
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

    public bool TryGetInInformation(PartId partId, [MaybeNullWhen(false)] out KeyValuePair<PartId, List<ModEntity>>? result)
    {
        if (Information == default || !Information.ContainsKey(partId))
        {
            result = default;
            return false;
        }
        result = Information.Single(p => p.Key == partId);
        return true;
    }
}
