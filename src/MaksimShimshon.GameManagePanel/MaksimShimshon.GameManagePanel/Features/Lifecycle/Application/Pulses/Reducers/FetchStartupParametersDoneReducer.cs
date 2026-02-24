using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class FetchStartupParametersDoneReducer : IReducer<GameInfoState, FetchStartupParametersDoneAction>
{
    public GameInfoState Reduce(GameInfoState state, FetchStartupParametersDoneAction action)
        => state with
        {
            StartupParameters = action.StartupParameters,
            SavedParametersLoaded = true
        };
}
