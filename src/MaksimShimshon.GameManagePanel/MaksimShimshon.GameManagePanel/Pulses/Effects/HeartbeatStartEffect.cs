using MaksimShimshon.GameManagePanel.Kernel.Heartbeat;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Pulses.Effects;

internal class HeartbeatStartEffect : IEffect<HeartbeatStartAction>
{
    private readonly IHeartbeatService _heartbeatService;
    private readonly IStateAccessor<HeartbeatState> _heartbeatStateAccess;

    public HeartbeatStartEffect(IHeartbeatService heartbeatService, IStateAccessor<HeartbeatState> heartbeatStateAccess)
    {
        _heartbeatService = heartbeatService;
        _heartbeatStateAccess = heartbeatStateAccess;
    }
    public Task EffectAsync(HeartbeatStartAction action, IDispatcher dispatcher)
    {
        if (_heartbeatStateAccess.State.IsBeating)
            return Task.CompletedTask;

        _ = _heartbeatService.StartBeatingAsync();
        return Task.CompletedTask;
    }
}
