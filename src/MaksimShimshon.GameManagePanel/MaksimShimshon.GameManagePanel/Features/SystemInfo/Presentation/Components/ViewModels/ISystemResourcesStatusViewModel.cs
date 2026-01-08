using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components.ViewModels;

public interface ISystemResourcesStatusViewModel : IWidgetViewModel
{
    public SystemInfoEntity? SystemInfo { get; }
    public DateTime LastUpdate { get; }
    public int Delay { get; }
}
