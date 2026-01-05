using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

public sealed class SystemInfoEntity
{
    public SystemMemory Memory { get; init; } = default!;
    public SystemDisk Disk { get; init; } = default!;
    public SystemProcessor Processor { get; init; } = default!;
}
