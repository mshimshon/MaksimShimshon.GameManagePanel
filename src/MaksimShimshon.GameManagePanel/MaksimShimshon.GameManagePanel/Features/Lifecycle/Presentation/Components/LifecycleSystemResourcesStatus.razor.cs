using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components;

public partial class LifecycleSystemResourcesStatus : ComponentBase
{
    [Inject] public ISwizzleFactory SwizzleFactory { get; set; } = default!;

    private LifecycleSystemResourcesStatusViewModel _viewModel = default!;
    protected override async Task OnInitializedAsync()
    {
        // Create or Get Exisintg Hook Binding
        var articleVMHook = SwizzleFactory.CreateOrGet<LifecycleSystemResourcesStatusViewModel>(() => this, ShouldUpdate);
        // Get View Model Type Instance of the Hook
        _viewModel = articleVMHook.GetViewModel<LifecycleSystemResourcesStatusViewModel>()!;
    }
    private Task ShouldUpdate() => InvokeAsync(StateHasChanged);


    private float CpuUsage => @MathF.Round((_viewModel.SystemState.SystemInfo?.Processor.Current ?? 0f) * 100, 0);
    private float RamUsage => @MathF.Round((_viewModel.SystemState.SystemInfo?.Memory.Percentage ?? 0f) * 100, 0);
    private float DiskUsage => @MathF.Round((_viewModel.SystemState.SystemInfo?.Disk.Percentage ?? 0f) * 100, 0);
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
