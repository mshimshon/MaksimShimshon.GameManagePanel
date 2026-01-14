using LunaticPanel.Core.Abstraction.Widgets;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public interface ISetupProcessViewModel : IWidgetViewModel
{
    public string KeyGame { get; set; }
    public IReadOnlyDictionary<string, string> AvailableInstallGames { get; }
    Task InitializeAsync();
    Task InstallAsync();
}
