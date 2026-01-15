using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Hooks.Queries;

[QueryBusId(LinuxGameServerKeys.Queries.GetServerInstallState)]
internal class GetInstallationStateHook : IQueryBusHandler
{
    private readonly IStateAccessor<InstallationState> _gameInstallState;
    public GetInstallationStateHook(IStateAccessor<InstallationState> gameInstallState)
    {
        _gameInstallState = gameInstallState;
    }

    public Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
        => qry.ReplyWith(_gameInstallState.State);
}
