using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.UI.Components;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
internal class WidgetModListSelectorHook : IEngineBusHandler
{
    private readonly IStateAccessor<ModListState> _modListStateAccess;

    public WidgetModListSelectorHook(IStateAccessor<ModListState> modListStateAccess)
    {
        _modListStateAccess = modListStateAccess;
    }
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetModlistSelector>(p => p with
        {
            VisibilityCondition = () => _modListStateAccess.State.Available.Count() > 0
        });
}
