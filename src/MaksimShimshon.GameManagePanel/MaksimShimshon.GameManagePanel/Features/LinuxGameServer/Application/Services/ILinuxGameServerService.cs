using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;

public interface ILinuxGameServerService
{
    Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default);

    Task<GameServerInfoEntity?> PerformServerInstallation(CancellationToken cancellation = default);
}
