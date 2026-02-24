using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

internal class ServerStatusUpdateEffect : IEffect<ServerStatusUpdateAction>
{
    private readonly IStateAccessor<ServerState> _stateAccessor;
    private readonly IMedihater _medihater;

    public ServerStatusUpdateEffect(
        IStateAccessor<ServerState> stateAccessor,
        IMedihater medihater)
    {
        _stateAccessor = stateAccessor;
        _medihater = medihater;
    }
    public async Task EffectAsync(ServerStatusUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetServerStatusQuery();
        var serverInfo = await _medihater.Send(exec);
        if (serverInfo != default)
        {
            bool isStartingTransitionCompleted = _stateAccessor.State.Transition == ServerTransition.Starting && serverInfo.Status == Status.Running;
            bool isStoppedTransitionCompleted = _stateAccessor.State.Transition == ServerTransition.Stopping && serverInfo.Status == Status.Stopped;

            if (isStartingTransitionCompleted || isStoppedTransitionCompleted)
                await dispatcher.Prepare<ServerStatusTransitionDoneAction>().DispatchAsync();
        }


        await dispatcher.Prepare<ServerStatusUpdateDoneAction>().With(p => p.ServerInfo, serverInfo).DispatchAsync();

    }
}
