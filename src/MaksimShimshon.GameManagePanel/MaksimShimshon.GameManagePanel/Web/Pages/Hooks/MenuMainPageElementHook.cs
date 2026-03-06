using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Web.Pages.Hooks.Components;

namespace MaksimShimshon.GameManagePanel.Web.Pages.Hooks;

[EngineBusId(MainMenuKeys.UI.GetElements)]
public class MenuMainPageElementHook : IEngineBusHandler
{

    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetMainPageMenuLink>();
}
