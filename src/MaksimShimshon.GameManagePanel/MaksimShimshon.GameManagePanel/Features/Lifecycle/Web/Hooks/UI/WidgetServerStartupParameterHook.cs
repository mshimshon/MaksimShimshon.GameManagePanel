using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.UI.Components;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
internal class WidgetServerStartupParameterHook : IEngineBusHandler
{
    private readonly IStateAccessor<GameInfoState> _stateGameInfoAccess;

    public WidgetServerStartupParameterHook(IStateAccessor<GameInfoState> stateGameInfoAccess)
    {
        _stateGameInfoAccess = stateGameInfoAccess;
    }
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetStartupParameters>(p => p with
        {
            VisibilityCondition = () => _stateGameInfoAccess.State.GameInfo != default
        });
}
