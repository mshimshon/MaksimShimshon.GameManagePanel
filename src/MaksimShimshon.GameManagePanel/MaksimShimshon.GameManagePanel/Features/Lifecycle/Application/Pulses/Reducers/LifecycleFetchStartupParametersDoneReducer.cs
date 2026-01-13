using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;

public class LifecycleFetchStartupParametersDoneReducer : IReducer<LifecycleGameInfoState, LifecycleFetchStartupParametersDoneAction>
{
    public LifecycleGameInfoState Reduce(LifecycleGameInfoState state, LifecycleFetchStartupParametersDoneAction action)
        => state with
        {
            StartupParameters = action.StartupParameters,
            SavedParametersLoaded = true
        };
}
