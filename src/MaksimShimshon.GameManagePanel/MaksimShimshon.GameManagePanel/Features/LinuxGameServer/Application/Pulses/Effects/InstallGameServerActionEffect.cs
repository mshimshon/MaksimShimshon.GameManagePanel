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
    public async Task EffectAsync(InstallGameServerAction action, IDispatcher dispatcher)
    {
        Console.WriteLine("Perform Install EFFECT");

        var result = await _medihater.Send(new InstallGameServerCommand(action.Id));
        Console.WriteLine("Perform Install AFTER MEDIATR");

        if (result == default)
            await dispatcher.Prepare<GameServerInstallFailedAction>()
                .With(p => p.Id, action.Id)
                .With(p => p.DisplayName, action.DisplayName)
                .DispatchAsync();
        else
            await dispatcher.Prepare<GameServerInstalledAction>().With(p => p.GameServerInstalled, result).DispatchAsync();

    }
}
