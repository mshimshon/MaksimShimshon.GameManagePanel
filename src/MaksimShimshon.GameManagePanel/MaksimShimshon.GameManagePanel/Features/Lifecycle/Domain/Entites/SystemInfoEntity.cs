using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

public sealed class SystemInfoEntity
{
    public SystemMemory Memory { get; init; } = default!;
    public SystemDisk Disk { get; init; } = default!;
    public SystemProcessor Processor { get; init; } = default!;
}
