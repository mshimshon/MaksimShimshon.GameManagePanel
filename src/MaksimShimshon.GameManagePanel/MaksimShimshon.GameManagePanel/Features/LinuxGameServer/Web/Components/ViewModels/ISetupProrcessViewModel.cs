using LunaticPanel.Core.Abstraction.Widgets;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public interface ISetupProrcessViewModel : IWidgetViewModel
{
    public string KeyGame { get; set; }
    public IReadOnlyDictionary<string, string> AvailableInstallGames { get; }
}
