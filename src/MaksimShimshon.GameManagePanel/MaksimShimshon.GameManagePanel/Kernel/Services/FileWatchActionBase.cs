using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Services;

public abstract record FileWatchActionBase : IAction
{
    public DateTime Date { get; set; }
    public FileWatchEvents Event { get; set; }
    public string FullName { get; set; } = default!;
    public string FileName { get; set; } = default!;
}
