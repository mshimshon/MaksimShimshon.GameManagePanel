using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;

internal class UpdateInstalledGameServerEffect : IEffect<UpdateInstalledGameServerAction>
{
    private readonly IMedihater _medihater;

    public UpdateInstalledGameServerEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(UpdateInstalledGameServerAction action, IDispatcher dispatcher)
    {
        var result = await _medihater.Send(new GetInstalledGameQuery());
        await dispatcher.Prepare<UpdateInstalledGameServerDoneAction>()
            .With(p => p.GameServerInfo, result)
            .DispatchAsync();
    }
}
