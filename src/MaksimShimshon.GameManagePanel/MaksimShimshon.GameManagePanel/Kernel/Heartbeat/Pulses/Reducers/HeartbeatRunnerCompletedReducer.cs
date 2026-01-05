using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Reducers;

internal class HeartbeatRunnerCompletedReducer : IReducer<HeartbeatState, HeartbeatRunnerCompletedAction>
{
    public Task<HeartbeatState> ReduceAsync(HeartbeatState state, HeartbeatRunnerCompletedAction action)
        => Task.FromResult(state with { LastCompletion = DateTime.UtcNow, IsRunning = false });
}
