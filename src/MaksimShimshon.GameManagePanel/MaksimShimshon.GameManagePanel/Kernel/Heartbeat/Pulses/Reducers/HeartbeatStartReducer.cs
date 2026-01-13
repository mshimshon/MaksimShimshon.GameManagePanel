using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Reducers;

internal class HeartbeatStartReducer : IReducer<HeartbeatState, HeartbeatStartAction>
{
    public HeartbeatState Reduce(HeartbeatState state, HeartbeatStartAction action)
        => state with
        {
            IsBeating = true
        };
}
