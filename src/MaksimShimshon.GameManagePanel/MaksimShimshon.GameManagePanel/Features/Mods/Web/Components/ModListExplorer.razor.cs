using MaksimShimshon.GameManagePanel.Core;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

public partial class ModListExplorer
{
    [Parameter]
    public bool AutoRedirect { get; set; } = false;

    [Parameter]
    public string LinkBase { get; set; } = $"/{BaseInfo.AssemblyName}/modlist";

    [Inject] public NavigationManager Navigation { get; set; } = default!;

    private async Task OpenModList(Guid id)
    {
        if (AutoRedirect)
            Navigation.NavigateTo($"{LinkBase}/{id}");
        else
            await ViewModel.GetAsync(id);
    }
}
