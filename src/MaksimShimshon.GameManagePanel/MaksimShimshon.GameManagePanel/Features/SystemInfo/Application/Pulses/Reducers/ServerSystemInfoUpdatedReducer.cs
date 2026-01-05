using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers;

public class ServerSystemInfoUpdatedReducer : IReducer<SystemInfoState, SystemInfoUpdatedAction>
{
    public async Task<SystemInfoState> ReduceAsync(SystemInfoState state, SystemInfoUpdatedAction action)
        => await Task.FromResult(state with { SystemInfo = action.SystemInfo });
}
