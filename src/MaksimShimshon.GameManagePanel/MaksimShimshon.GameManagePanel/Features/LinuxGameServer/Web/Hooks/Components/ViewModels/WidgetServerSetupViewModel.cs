using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components.ViewModels;

public class WidgetServerSetupViewModel : WidgetViewModelBase, IWidgetServerSetupViewModel
{
    private readonly IStatePulse _statePulse;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateChanges);
    public WidgetServerSetupViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }

}
