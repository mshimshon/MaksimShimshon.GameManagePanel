using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components.ViewModels;

public interface ISystemResourcesStatusViewModel : IWidgetViewModel
{
    SystemInfoEntity? SystemInfo { get; }
    DateTime LastUpdate { get; }
}
