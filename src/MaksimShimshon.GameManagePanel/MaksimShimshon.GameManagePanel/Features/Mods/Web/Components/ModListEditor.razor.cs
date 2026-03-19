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

    private async Task DeleteEntity(PartId partId, ModEntity toDelete)
    {
        ViewModel.RemoveFrom(partId, toDelete);
    }
}
