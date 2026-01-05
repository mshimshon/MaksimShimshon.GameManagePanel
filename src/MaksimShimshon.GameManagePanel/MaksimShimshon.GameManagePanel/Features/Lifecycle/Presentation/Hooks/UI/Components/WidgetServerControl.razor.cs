using LunaticPanel.Core;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components;

public partial class WidgetServerControl : IDisposable
{

    private readonly IPluginService<PluginEntry> _pluginService;
    public WidgetServerControl(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }
    public WidgetServerControlViewModel ViewModel { get; set; } = default!;

    protected override void OnInitialized()
    {
        ViewModel = _pluginService.GetRequired<WidgetServerControlViewModel>();
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
