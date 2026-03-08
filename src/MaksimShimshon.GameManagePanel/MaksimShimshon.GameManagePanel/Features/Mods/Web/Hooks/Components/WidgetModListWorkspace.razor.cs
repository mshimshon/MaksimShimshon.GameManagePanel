using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components;

public partial class WidgetModListWorkspace
{
    [Parameter] public Guid FileIdentifier { get; set; }
    private Guid _initialFileId;
    protected override void OnWidgetInitialized()
    {
        _initialFileId = FileIdentifier;
    }
    protected override async Task OnWidgetParametersSetAsync()
    {
        if (_initialFileId != FileIdentifier)
            await ViewModel.LoadAsync(FileIdentifier);

    }

    protected override async Task OnWidgetAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await ViewModel.LoadAsync(FileIdentifier);
    }

}
