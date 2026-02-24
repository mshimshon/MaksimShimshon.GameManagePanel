using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;

namespace MaksimShimshon.GameManagePanel.Kernel.Services;

internal sealed record FileTrackerKey
{

    public string FilePath { get; }
    public FileWatchEvents FileWatchEvents { get; }

    public FileTrackerKey(string filePath, FileWatchEvents fileWatchEvents)
    {
        FilePath = filePath;
        FileWatchEvents = fileWatchEvents;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FilePath, FileWatchEvents);
    }
}
