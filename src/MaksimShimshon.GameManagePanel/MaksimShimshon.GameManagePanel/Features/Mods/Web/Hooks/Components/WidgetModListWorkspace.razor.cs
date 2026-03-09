using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components;

public partial class WidgetModListWorkspace
{
    [Parameter] public Guid ModListId { get; set; }
    protected override async Task OnWidgetParametersSetAsync()
    {
        ViewModel.ModListId = ModListId;
    }


}
