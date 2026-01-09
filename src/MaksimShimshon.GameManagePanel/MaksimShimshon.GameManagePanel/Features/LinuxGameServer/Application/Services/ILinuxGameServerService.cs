namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;

public interface ILinuxGameServerService
{
    Task<IReadOnlyCollection<string>> GetAvailableGames(CancellationToken cancellation = default);
}
