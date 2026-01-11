using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;

public record InstallationState : IStateFeatureSingleton
{
    public bool IsInstallationCompleted => GameServerInfo != default && InProgressInstallation == default;
    public GameServerInfoEntity? GameServerInfo { get; init; }
    public GameServerInstallProcessModel? InProgressInstallation { get; init; }

}
