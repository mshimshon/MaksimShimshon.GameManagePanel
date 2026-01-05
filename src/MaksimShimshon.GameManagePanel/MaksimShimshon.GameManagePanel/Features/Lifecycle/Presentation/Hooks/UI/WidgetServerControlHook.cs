using LunaticPanel.Core.Extenstions;
using LunaticPanel.Core.Messaging.EngineBus;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
internal class WidgetServerControlHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWith<WidgetServerControl>();
}
