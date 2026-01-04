using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

public record LifecycleServerSystemInfoUpdatedAction : IAction
{
    public SystemInfoEntity SystemInfo { get; set; } = default!;
}
