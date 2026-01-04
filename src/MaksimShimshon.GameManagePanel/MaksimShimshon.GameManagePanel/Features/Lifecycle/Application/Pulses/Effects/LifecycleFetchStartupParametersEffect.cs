using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Queries;
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
        try
        {
            data = await _medihater.Send(exec);
        }
        finally
        {
            var dispatchPrep = dispatcher.Prepare<LifecycleFetchStartupParametersDoneAction>();
            dispatchPrep.With(p => p.StartupParameters, data);
            await dispatchPrep.DispatchAsync();
        }



    }
}
