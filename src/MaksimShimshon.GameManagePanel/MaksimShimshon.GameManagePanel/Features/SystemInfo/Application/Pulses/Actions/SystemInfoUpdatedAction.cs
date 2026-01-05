using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;

public record SystemInfoUpdatedAction : IAction
{
    public SystemInfoEntity SystemInfo { get; set; } = default!;
}
