using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;

public record InstallGameServerActionEffect : IEffect<InstallGameServerAction>
{
    private readonly IMedihater _medihater;

    public InstallGameServerActionEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public Task EffectAsync(InstallGameServerAction action, IDispatcher dispatcher)
    {
        _ = _medihater.Send(new InstallGameServerCommand(action.Id, action.DisplayName));

        return Task.CompletedTask;
    }
}
