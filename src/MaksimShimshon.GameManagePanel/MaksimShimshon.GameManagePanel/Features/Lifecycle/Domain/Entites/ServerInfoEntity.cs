using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

public sealed record ServerInfoEntity
{
    public Status Status { get; init; } = Status.Stopped;
    public string Name { get; init; } = default!;
    public string Ip { get; init; } = default!;
    public string Port { get; init; } = default!;
    public DateTime LastUpdate { get; init; }
    public string Pid { get; init; }
    public GameInfoEntity GameInfo { get; init; } = default!;
    public SystemInfoEntity? SystemInfo { get; init; }
}
