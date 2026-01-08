using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services;

internal class LinuxSystemInfoService : ISystemInfoService
{
    public Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
        => Task.FromResult(
            new SystemInfoEntity()
            {
                Disk = new(1000, 10000),
                Memory = new(1000, 2000),
                Processor = new Domain.ValueObjects.SystemProcessor(50, 4, "Test @ 2.5Ghz")
            }
            );
}
