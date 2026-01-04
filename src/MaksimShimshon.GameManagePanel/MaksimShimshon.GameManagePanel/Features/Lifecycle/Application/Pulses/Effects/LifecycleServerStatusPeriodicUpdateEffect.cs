using GameServerManager.Core.Abstractions.Ticker.Pulses.Actions;
using GameServerManager.Features.Lifecycle.Application.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Stores;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class LifecycleServerStatusPeriodicUpdateEffect : IEffect<TickerPerformerAction>
{
    private readonly IStateAccessor<LifecycleServerState> _stateAccessor;
    private readonly IStateAccessor<LifecycleGameInfoState> _lifecycleGameInfoStateAccessor;
    private readonly IMedihater _medihater;

    public LifecycleServerStatusPeriodicUpdateEffect(IStateAccessor<LifecycleServerState> stateAccessor, IStateAccessor<LifecycleGameInfoState> lifecycleGameInfoStateAccessor, IMedihater medihater)
    {
        _stateAccessor = stateAccessor;
        _lifecycleGameInfoStateAccessor = lifecycleGameInfoStateAccessor;
        _medihater = medihater;
    }
    public async Task EffectAsync(TickerPerformerAction action, IDispatcher dispatcher) {
        DateTime nextUpdated = _stateAccessor.State.ServerInfoLastUpdate.AddSeconds(_stateAccessor.State.Delay);

        if (nextUpdated > DateTime.UtcNow)
        {
            return;
        }
        if (_stateAccessor.State.SkipNextUpdates > 1)
        {
            await dispatcher.Prepare<LifecycleServerStatusUpdateSkippedAction>().DispatchAsync();
            return;
        }

        var exec = new GetServerStatusQuery();
        var serverInfo = await _medihater.Send(exec);

        if (_lifecycleGameInfoStateAccessor.State.GameInfo == default && serverInfo.GameInfo != default)
        {
            await dispatcher.Prepare<LifecycleServerGameInfoUpdatedAction>()
                .With(p => p.GameInfo, serverInfo.GameInfo)
                .Await()
                .DispatchAsync();
            await dispatcher.Prepare<LifecycleFetchStartupParametersAction>()
                .DispatchAsync();
        }

        if (serverInfo.SystemInfo != default  && serverInfo.SystemInfo.Disk != default && serverInfo.SystemInfo.Processor != default && serverInfo.SystemInfo.Memory != default)
            await dispatcher.Prepare<LifecycleServerSystemInfoUpdatedAction>()
                .With(p => p.SystemInfo, serverInfo.SystemInfo)
                .DispatchAsync();

        var dispatchPrep = dispatcher.Prepare<LifecycleServerStatusUpdateDoneAction>();
        dispatchPrep.With(p => p.ServerInfo, serverInfo);
        await dispatchPrep.DispatchAsync();
        if (_stateAccessor.State.SkipNextUpdates > 0)
            await dispatcher.Prepare<LifecycleServerStatusUpdateSkippedAction>().DispatchAsync();


    }
}
