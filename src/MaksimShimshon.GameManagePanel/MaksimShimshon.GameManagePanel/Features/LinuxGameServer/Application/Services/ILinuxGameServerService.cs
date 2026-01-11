namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;

public interface ILinuxGameServerService
{
    Task<Dictionary<string, string>> GetAvailableGames(CancellationToken cancellation = default);
}
