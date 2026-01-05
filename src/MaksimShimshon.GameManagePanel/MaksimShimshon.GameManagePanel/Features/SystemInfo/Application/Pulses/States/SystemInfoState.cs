using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;

public record SystemInfoState : IStateFeature
{
    public SystemInfoEntity? SystemInfo { get; init; }
    public DateTime LastUpdate { get; init; }
    public int Delay { get; init; } = 8;

}
