using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;

public interface ILinuxGameServerService
{
    Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default);

    Task<GameServerInfoEntity?> PerformServerInstallation(string gameServer, CancellationToken cancellation = default);

    Task<GameServerInstallProcessModel?> GetInstallationProgress(CancellationToken cancellation = default);
    Task<GameServerInfoEntity?> GetInstalledGameServer(CancellationToken cancellation = default);



}
