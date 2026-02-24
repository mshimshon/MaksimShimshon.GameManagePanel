using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

public class ServerStartEffect : IEffect<ServerStartAction>
{
    private readonly IMedihater _medihater;

    public ServerStartEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(ServerStartAction action, IDispatcher dispatcher)
    {
        var exec = new ExecStartServerCommand();
        await _medihater.Send(exec);
        await dispatcher
            .Prepare<ServerStartDoneAction>()
            .DispatchAsync();
    }
}
