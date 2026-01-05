using LunaticPanel.Core;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Hooks.Components;

partial class WidgetSystemInfo : IDisposable
{
    private readonly IPluginService<PluginEntry> _pluginService;
    public WidgetSystemInfo(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }

    public WidgetSystemInfoViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel = _pluginService.GetRequired<WidgetSystemInfoViewModel>();
        ViewModel.SpreadChanges += ShouldUpdate;
    }

    private Task ShouldUpdate() => InvokeAsync(StateHasChanged);
    public void Dispose()
    {
        ViewModel.SpreadChanges -= ShouldUpdate;
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

    }
}
