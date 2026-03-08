using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;

public partial class ModListExplorer
{
    [Parameter] public EventCallback<Guid> OnEditClick { get; set; }

}
