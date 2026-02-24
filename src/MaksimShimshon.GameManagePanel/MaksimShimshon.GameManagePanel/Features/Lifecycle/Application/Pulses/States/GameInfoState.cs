using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;

public record GameInfoState : IStateFeatureSingleton
{
    public GameInfoEntity? GameInfo { get; init; }
    public Dictionary<string, string> StartupParameters { get; init; } = new();
    public bool SavedParametersLoaded { get; init; }
}
