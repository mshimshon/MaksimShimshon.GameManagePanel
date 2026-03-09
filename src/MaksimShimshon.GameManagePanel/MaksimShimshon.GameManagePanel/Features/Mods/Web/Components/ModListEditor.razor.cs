using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

public partial class ModListEditor
{
    [Parameter] public Guid Id { get; set; }
    protected override void OnWidgetInitialized()
    {
        ViewModel.Id = Id;
    }

    protected override async Task OnWidgetAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await ViewModel.LoadAsync(true);
    }

    private void ItemUpdated(MudItemDropInfo<ModEntity> dropItem)
    {
        Console.WriteLine($"HEY DROPPED: {dropItem.IndexInZone}");
        MoveTo(dropItem.Item!, dropItem.IndexInZone);
        foreach (var item in _items)
        {
            Console.WriteLine($"HEY DROP: {item.Id}");
        }
    }


}
