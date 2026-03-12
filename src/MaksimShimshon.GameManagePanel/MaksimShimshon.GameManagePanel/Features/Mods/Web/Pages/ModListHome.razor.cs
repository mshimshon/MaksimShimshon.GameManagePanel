using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Pages;

public partial class ModListHome
{
    [Parameter]
    public Guid? Id { get; set; }

    protected override async Task OnWidgetParametersSetAsync()
    {
        if (Id == default) return;
        if (FirstRenderCompleted)
            await ViewModel.GetAsync((Guid)Id!);

    }
    protected override async Task OnWidgetAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Id != default)
                await ViewModel.GetAsync((Guid)Id!);
        }
    }
}
