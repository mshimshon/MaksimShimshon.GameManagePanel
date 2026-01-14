using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components.ViewModels;

public interface IWidgetServerSetupViewModel : IWidgetViewModel
{
    InstallationState InstallState { get; }
}
