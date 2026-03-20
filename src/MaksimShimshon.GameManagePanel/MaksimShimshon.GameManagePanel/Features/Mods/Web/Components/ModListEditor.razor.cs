using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MudBlazor;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

public partial class ModListEditor
{
    private const string ERROR_SCHEMATIC_PARTS = "It Looks like the Mod Schematic had an error loading up."; // TODO: Localize
    private void ItemUpdated(MudItemDropInfo<ModEntity> dropItem)
        => ViewModel.MoveTo(new PartId(dropItem.DropzoneIdentifier), dropItem.Item!, dropItem.IndexInZone);
    int _counter = 0;
    private async Task AddInto(PartId partId)
    {
        // TODO: Show Dialog
        ViewModel.AddTo(partId, new ModEntity(new ModId($"WhatEver_{_counter}"), null));
        _counter++;
    }

    private async Task<bool> DeleteEntity(PartId partId, ModEntity toDelete, CancellationToken ct = default)
    {
        try
        {
            await Task.Delay(2000);
            //ViewModel.RemoveFrom(partId, toDelete);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<bool> Restore(PartId partId, ModEntity toDelete, CancellationToken ct = default)
    {
        return true;
    }
}
