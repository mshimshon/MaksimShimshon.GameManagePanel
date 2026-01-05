using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;

public record LifecycleServerState : IStateFeatureSingleton
{
    public ServerInfoEntity? ServerInfo { get; init; }
    public ServerTransition Transition { get; init; } = ServerTransition.Idle;
    public int TransitionTicks { get; init; }

    public DateTime ServerInfoLastUpdate { get; init; }
    public string? LastRunErrorCode { get; init; }
    public string? LastRunErrorMessage { get; init; }
    public int SkipNextUpdates { get; init; }
    public int Delay { get; init; } = 8;
}
