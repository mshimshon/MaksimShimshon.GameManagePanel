using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services;

internal class LifecycleServices : ILifecycleServices
{
    public Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(new Dictionary<string, string>());

    public Task ServerRestartAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task ServerStartAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(default(ServerInfoEntity));

    public Task ServerStopAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task UpdateStartupParameterAsync(string key, string value, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
