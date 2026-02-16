using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Core.UI;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Hooks;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
public class WidgetServerSetupHook : IEngineBusHandler
{
    private readonly IStateAccessor<InstallationState> _installStateAccess;

    public WidgetServerSetupHook(IStateAccessor<InstallationState> installStateAccess)
    {
        _installStateAccess = installStateAccess;
    }
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetServerSetup>((msg) => msg with
        {
            VisibilityCondition = () => !_installStateAccess.State.IsInstallationCompleted
        });
}
