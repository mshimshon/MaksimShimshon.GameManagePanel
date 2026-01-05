using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Messaging.EngineBus;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Hooks.Components;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Hooks;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
public class WidgetSystemInfoHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWith<WidgetSystemInfo>();
}
