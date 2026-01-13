using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Reducers;

internal class HeartbeatRunnerCompletedReducer : IReducer<HeartbeatState, HeartbeatRunnerCompletedAction>
{
    public HeartbeatState Reduce(HeartbeatState state, HeartbeatRunnerCompletedAction action)
        => state with
        {
            LastCompletion = DateTime.UtcNow,
            IsRunning = false
        };
}
