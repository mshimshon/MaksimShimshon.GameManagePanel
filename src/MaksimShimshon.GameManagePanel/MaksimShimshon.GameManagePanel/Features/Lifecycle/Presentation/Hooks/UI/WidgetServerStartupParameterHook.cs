using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
internal class WidgetServerStartupParameterHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWith<WidgetStartupParameters>();
}
