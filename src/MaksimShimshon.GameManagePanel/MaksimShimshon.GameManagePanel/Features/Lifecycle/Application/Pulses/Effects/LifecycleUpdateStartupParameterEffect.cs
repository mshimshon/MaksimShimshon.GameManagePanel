using GameServerManager.Features.Lifecycle.Application.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class LifecycleUpdateStartupParameterEffect : IEffect<LifecycleUpdateStartupParameterAction>
{
    private readonly IMedihater _medihater;

    public LifecycleUpdateStartupParameterEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(LifecycleUpdateStartupParameterAction action, IDispatcher dispatcher)
    {
        var exec = new ExecUpdateStartupParameterCommand() {
            Key = action.Key,
            Value = action.Value
        };
        await _medihater.Send(exec);

    }
}
