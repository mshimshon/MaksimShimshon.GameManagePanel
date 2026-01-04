using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class LifecycleServerStopEffect : IEffect<LifecycleServerStopAction>
{
    private readonly IMedihater _medihater;

    public LifecycleServerStopEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(LifecycleServerStopAction action, IDispatcher dispatcher)
    {
        var exec = new ExecStopServerCommand();
        await _medihater.Send(exec);

        await dispatcher.Prepare<LifecycleServerStopDoneAction>().DispatchAsync();
    }
}
