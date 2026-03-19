namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;

public interface IStartupParameterService
{
    Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken cancellationToken = default);
    Task UpdateStartupParameterAsync(string key, string value, CancellationToken cancellationToken = default);
}
