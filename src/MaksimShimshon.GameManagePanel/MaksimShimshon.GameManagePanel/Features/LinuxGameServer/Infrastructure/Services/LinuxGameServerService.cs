using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    public Task<IReadOnlyCollection<string>> GetAvailableGames(CancellationToken cancellation = default) => throw new NotImplementedException();
}
