using LunaticPanel.Core;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components;

public partial class WidgetStartupParameters : IDisposable
{

    private readonly IPluginService<PluginEntry> _pluginService;
    public WidgetStartupParameters(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }
    public WidgetStartupParametersViewModel ViewModel { get; set; } = default!;

    protected override void OnInitialized()
    {
        ViewModel = _pluginService.GetRequired<WidgetStartupParametersViewModel>();
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