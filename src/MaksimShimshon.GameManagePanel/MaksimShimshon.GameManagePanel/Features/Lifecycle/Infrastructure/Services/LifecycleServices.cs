using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services;

internal class LifecycleServices : ILifecycleServices
{
    public Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task ServerRestartAsync(CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task ServerStartAsync(CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task ServerStopAsync(CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task UpdateStartupParameterAsync(string key, string value, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
