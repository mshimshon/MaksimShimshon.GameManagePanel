using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

public sealed record ServerInfoEntity
{
    public Status Status { get; init; }
    public string? Name { get; init; }
    public string? Ip { get; init; }
    public string? Port { get; init; }
    public DateTime LastUpdate { get; init; }
    public string? Pid { get; init; }
    public GameInfoEntity? GameInfo { get; init; }
    public SystemInfoEntity? SystemInfo { get; init; }
    public ServerInfoEntity(Status status)
    {
        Status = status;
    }
}
