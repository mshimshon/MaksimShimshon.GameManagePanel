using LunaticPanel.Core;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Pages;

public partial class Lifecycle : ComponentBase, IDisposable
{

    private readonly IPluginService<PluginEntry> _pluginService;
    public Lifecycle(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }
    public LifecycleViewModel ViewModel { get; set; } = default!;

    protected override void OnInitialized()
    {
        ViewModel = _pluginService.GetRequired<LifecycleViewModel>();
        ViewModel.SpreadChanges += ShouldUpdate;

    }
    private Task ShouldUpdate() => InvokeAsync(StateHasChanged);
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        ViewModel.SpreadChanges -= ShouldUpdate;
    }

}
