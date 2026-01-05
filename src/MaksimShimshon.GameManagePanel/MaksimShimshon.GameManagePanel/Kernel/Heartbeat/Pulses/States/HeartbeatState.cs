using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;

public record HeartbeatState : IStateFeature
{
    public DateTime LastCompletion { get; init; }
    public DateTime LastStart { get; init; }
    public TimeSpan Elapsed => LastStart - LastCompletion;
    public bool IsBeating { get; init; }
    public bool IsRunning { get; init; }

}
