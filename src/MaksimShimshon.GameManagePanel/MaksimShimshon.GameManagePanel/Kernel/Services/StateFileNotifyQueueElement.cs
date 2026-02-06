namespace MaksimShimshon.GameManagePanel.Kernel.Services;

internal sealed record StateFileNotifyQueueElement
{
    public string Path { get; init; } = string.Empty;
    public long Version { get; init; }
    public Func<Task> Action { get; init; } = default!;
}
