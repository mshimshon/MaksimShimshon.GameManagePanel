using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.UI.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
internal class WidgetServerControlHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWith<WidgetServerControl>();
}
