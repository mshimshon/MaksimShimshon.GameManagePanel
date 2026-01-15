using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public interface ISetupProcessViewModel : IWidgetViewModel
{
    public InstallationState InstallState { get; }
    public string KeyGame { get; set; }
    public string RepositoryTarget { get; }
    Task InstallAsync();
}
