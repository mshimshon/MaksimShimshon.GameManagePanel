using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Reducers;

internal class HeartbeatStartReducer : IReducer<HeartbeatState, HeartbeatStartAction>
{
    public Task<HeartbeatState> ReduceAsync(HeartbeatState state, HeartbeatStartAction action)
        => Task.FromResult(state with { IsBeating = true });
}
