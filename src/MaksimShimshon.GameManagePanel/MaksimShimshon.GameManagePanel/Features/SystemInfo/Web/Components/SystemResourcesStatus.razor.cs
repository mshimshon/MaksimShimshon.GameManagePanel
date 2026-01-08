
namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components;

public partial class SystemResourcesStatus
{
    private float CpuUsage => @MathF.Round((ViewModel?.SystemInfo?.Processor.Current ?? 0f) * 100, 0);
    private float RamUsage => @MathF.Round((ViewModel?.SystemInfo?.Memory.Percentage ?? 0f) * 100, 0);
    private float DiskUsage => @MathF.Round((ViewModel?.SystemInfo?.Disk.Percentage ?? 0f) * 100, 0);
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await ViewModel.StartPeriodicUpdates();
    }
}
