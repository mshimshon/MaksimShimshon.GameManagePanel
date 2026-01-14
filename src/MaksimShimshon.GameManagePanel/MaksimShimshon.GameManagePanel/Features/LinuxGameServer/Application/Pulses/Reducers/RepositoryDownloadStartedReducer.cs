using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class RepositoryDownloadStartedReducer : IReducer<GameRepositoryState, RepositoryDownloadStartedAction>
{
    public GameRepositoryState Reduce(GameRepositoryState state, RepositoryDownloadStartedAction action)
        => state with
        {
            IsInitializing = true
        };
}
