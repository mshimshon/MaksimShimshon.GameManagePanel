using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Hooks;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
public class WidgetServerSetupHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWith<WidgetServerSetup>();
}
