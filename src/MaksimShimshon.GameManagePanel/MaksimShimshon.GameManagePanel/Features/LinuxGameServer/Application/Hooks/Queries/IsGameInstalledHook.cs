using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Hooks.Queries;

[QueryBusId(LinuxGameServerKeys.Queries.IsGameServerInstalled)]
internal class IsGameInstalledHook : IQueryBusHandler
{
    private readonly IStateAccessor<InstallationState> _gameInstallState;
    public IsGameInstalledHook(IStateAccessor<InstallationState> gameInstallState)
    {
        _gameInstallState = gameInstallState;
    }

    public Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
        => qry.ReplyWith(_gameInstallState.State.GameServerInfo != default);
}
