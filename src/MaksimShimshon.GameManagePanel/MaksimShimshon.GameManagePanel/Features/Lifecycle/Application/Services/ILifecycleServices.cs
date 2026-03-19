using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;

public interface ILifecycleServices
{
    Task ServerStartAsync(CancellationToken cancellationToken = default);
    Task ServerStopAsync(CancellationToken cancellationToken = default);
    Task ServerRestartAsync(CancellationToken cancellationToken = default);
    Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken cancellationToken = default);

}
