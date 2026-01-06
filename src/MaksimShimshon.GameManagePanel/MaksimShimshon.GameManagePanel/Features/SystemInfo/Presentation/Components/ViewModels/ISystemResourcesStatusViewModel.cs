using LunaticPanel.Core.Widgets;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components.ViewModels;

public interface ISystemResourcesStatusViewModel : IViewModel
{
    public SystemInfoEntity? SystemInfo { get; }
    public DateTime LastUpdate { get; }
    public int Delay { get; }
}
