using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class LifecycleFetchStartupParametersEffect : IEffect<LifecycleFetchStartupParametersAction>
{
    private readonly IMedihater _medihater;

    public LifecycleFetchStartupParametersEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(LifecycleFetchStartupParametersAction action, IDispatcher dispatcher)
    {
        var exec = new GetStartupParametersQuery();
        Dictionary<string, string>? data = default;
        data = await _medihater.Send(exec);
        await dispatcher
            .Prepare<LifecycleFetchStartupParametersDoneAction>()
            .With(p => p.StartupParameters, data)
            .DispatchAsync();
    }
}
