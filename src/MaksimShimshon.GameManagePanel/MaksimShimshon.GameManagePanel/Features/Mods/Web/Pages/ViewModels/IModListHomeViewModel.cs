using LunaticPanel.Core.Abstraction.Widgets;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Pages.ViewModels;

public interface IModListHomeViewModel : IWidgetViewModel
{
    Guid InitialId { get; set; }
    Task GetAsync(Guid id);
}
