using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;

public interface ISystemInfoService
{
    Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default);
}
