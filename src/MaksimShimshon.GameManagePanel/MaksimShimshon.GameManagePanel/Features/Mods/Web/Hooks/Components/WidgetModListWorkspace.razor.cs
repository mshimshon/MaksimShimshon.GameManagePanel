using MaksimShimshon.GameManagePanel.Core;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components;

public partial class WidgetModListWorkspace
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Parameter] public bool AutoRedirect { get; set; } = false;
    [Parameter] public string LinkBase { get; set; } = $"/{BaseInfo.AssemblyName}/modlist";
    protected override async Task OnWidgetParametersSetAsync()
    {
    }

}
