using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;

public record InstallationState : IStateFeatureSingleton
{
    public bool IsLoading { get; init; }
    public GameServerInfoEntity? GameServerInfo { get; init; }
    public string? Error { get; init; }
}
