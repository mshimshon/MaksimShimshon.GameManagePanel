using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class LifecycleServerStartEffect : IEffect<LifecycleServerStartAction>
{
    private readonly IMedihater _medihater;

    public LifecycleServerStartEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(LifecycleServerStartAction action, IDispatcher dispatcher)
    {
        var exec = new ExecStartServerCommand();
        await _medihater.Send(exec);
        await dispatcher
            .Prepare<LifecycleServerStartDoneAction>()
            .DispatchAsync();
    }
}
