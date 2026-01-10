using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public class SetupProrcessViewModel : WidgetViewModelBase, ISetupProrcessViewModel
{
    private readonly IStatePulse _statePulse;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateChanges);
    public SetupProrcessViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
        _statePulse = statePulse;
    }
}
