using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services;

internal class SystemInfoService : ISystemInfoService
{
    public Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
        => Task.FromResult(default(SystemInfoEntity));
}
