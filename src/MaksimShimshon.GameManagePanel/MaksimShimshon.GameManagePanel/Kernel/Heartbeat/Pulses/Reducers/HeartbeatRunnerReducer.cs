using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Reducers;

internal class HeartbeatRunnerReducer : IReducer<HeartbeatState, HeartbeatRunnerAction>
{
    public HeartbeatState Reduce(HeartbeatState state, HeartbeatRunnerAction action)
        => state with
        {
            LastStart = DateTime.UtcNow,
            IsRunning = true
        };
}
