using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class UpdateStartupParameterEffect : IEffect<UpdateStartupParameterAction>
{
    private readonly IMedihater _medihater;

    public UpdateStartupParameterEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(UpdateStartupParameterAction action, IDispatcher dispatcher)
    {
        var exec = new ExecUpdateStartupParameterCommand()
        {
            Key = action.Key,
            Value = action.Value
        };
        await _medihater.Send(exec);

    }
}
