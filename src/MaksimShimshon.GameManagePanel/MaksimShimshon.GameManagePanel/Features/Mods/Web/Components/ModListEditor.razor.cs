using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MudBlazor;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

public partial class ModListEditor
{

    private void ItemUpdated(MudItemDropInfo<ModEntity> dropItem)
        => ViewModel.MoveTo(dropItem.DropzoneIdentifier, dropItem.Item!, dropItem.IndexInZone);


}
