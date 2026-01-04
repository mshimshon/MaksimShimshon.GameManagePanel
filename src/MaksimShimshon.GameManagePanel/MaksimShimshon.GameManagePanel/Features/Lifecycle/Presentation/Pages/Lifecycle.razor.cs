using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Pages;

public partial class Lifecycle : ComponentBase
{
    [Inject] public LifecycleViewModel ViewModel { get; set; } = default!;
}
