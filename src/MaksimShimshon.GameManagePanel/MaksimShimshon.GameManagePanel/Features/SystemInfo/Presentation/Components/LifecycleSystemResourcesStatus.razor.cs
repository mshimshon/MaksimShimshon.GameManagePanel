using LunaticPanel.Core;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components;

public partial class LifecycleSystemResourcesStatus : ComponentBase, IDisposable
{
    private readonly IPluginService<PluginEntry> _pluginService;
    public LifecycleSystemResourcesStatus(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }

    public LifecycleSystemResourcesStatusViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel = _pluginService.GetRequired<LifecycleSystemResourcesStatusViewModel>();
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
    private float CpuUsage => @MathF.Round((ViewModel.SystemState.SystemInfo?.Processor.Current ?? 0f) * 100, 0);
    private float RamUsage => @MathF.Round((ViewModel.SystemState.SystemInfo?.Memory.Percentage ?? 0f) * 100, 0);
    private float DiskUsage => @MathF.Round((ViewModel.SystemState.SystemInfo?.Disk.Percentage ?? 0f) * 100, 0);
    public static string FormatMegabytes(float mb)
    {
        if (mb < 1024)
            return $"{mb:0.##} MB";

        float gb = mb / 1024;
        if (gb < 1024)
            return $"{gb:0.##} GB";

        float tb = gb / 1024;
        return $"{tb:0.##} TB";
    }
}
