using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

internal class LifecycleServerGameInfoUpdateEffect : IEffect<LifecycleServerGameInfoUpdateAction>
{
    private readonly IMedihater _medihater;

    public LifecycleServerGameInfoUpdateEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(LifecycleServerGameInfoUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetGameInfoQuery();
        var gameInfo = await _medihater.Send(exec);
        if (gameInfo != default)
        {
            //TODO: MONITOR CONSISTENCY OF STATE AS TWO ACTION CHANGES THE SAME STATE DIFFERENT PROPS.
            await dispatcher.Prepare<LifecycleServerGameInfoUpdateDoneAction>()
                .With(p => p.GameInfo, gameInfo)
                .DispatchAsync();
            await dispatcher.Prepare<LifecycleFetchStartupParametersAction>()
                .DispatchAsync();
        }

    }
}
