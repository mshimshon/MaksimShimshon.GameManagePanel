using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers;

public class ServerSystemInfoUpdatedReducer : IReducer<SystemInfoState, SystemInfoUpdatedAction>
{
    public SystemInfoState Reduce(SystemInfoState state, SystemInfoUpdatedAction action)
    => state with { SystemInfo = action.SystemInfo, LastUpdate = DateTime.UtcNow };
}
