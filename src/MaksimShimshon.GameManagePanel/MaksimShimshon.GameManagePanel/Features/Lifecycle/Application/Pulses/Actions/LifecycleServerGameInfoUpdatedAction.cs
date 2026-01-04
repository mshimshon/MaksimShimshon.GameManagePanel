using GameServerManager.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

public record LifecycleServerGameInfoUpdatedAction : IAction
{
    public GameInfoEntity GameInfo { get; set; } = default!;
}
