using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;

public record InstallGameServerActionEffect : IEffect<InstallGameServerAction>
{
    public async Task EffectAsync(InstallGameServerAction action, IDispatcher dispatcher)
    {

    }
}
