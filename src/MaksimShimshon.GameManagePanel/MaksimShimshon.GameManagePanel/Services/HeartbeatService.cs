using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Services;

internal class HeartbeatService : IHeartbeatService
{
    private readonly IStateAccessor<HeartbeatState> _heartbeatStateAccessor;
    private readonly IDispatcher _dispatcher;
    private readonly int _internval = 1000;
    public HeartbeatService(IStateAccessor<HeartbeatState> heartbeatStateAccessor, Configuration configuration, IDispatcher dispatcher)
    {
        _heartbeatStateAccessor = heartbeatStateAccessor;
        _dispatcher = dispatcher;
        if (configuration.Heartbeat != default && configuration.Heartbeat.Interval > 1000)
            _internval = configuration.Heartbeat.Interval;
    }

    public async Task StartBeatingAsync(CancellationToken ct = default)
    {

        do
        {

            await _dispatcher
                .Prepare<HeartbeatRunnerAction>()
                .Await()
                .DispatchAsync();

            await Task.Delay(_internval);
        } while (_heartbeatStateAccessor.State.IsBeating);
    }
}
