using MaksimShimshon.GameManagePanel.Kernel.Dto;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record StatusServerResponse : ScriptResponse
{
    public int Status { get; init; }
    public string? Name { get; init; }
    public string? Ip { get; init; }
    public string? Port { get; init; }
    public string? Pid { get; init; }
}
