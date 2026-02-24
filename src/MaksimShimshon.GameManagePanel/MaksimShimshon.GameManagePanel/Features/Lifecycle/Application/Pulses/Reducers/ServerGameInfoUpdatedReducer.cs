using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class ServerGameInfoUpdatedReducer : IReducer<GameInfoState, ServerGameInfoUpdateDoneAction>
{

    public GameInfoState Reduce(GameInfoState state, ServerGameInfoUpdateDoneAction action)
        => state with
        {
            GameInfo = action.GameInfo
        };
}
