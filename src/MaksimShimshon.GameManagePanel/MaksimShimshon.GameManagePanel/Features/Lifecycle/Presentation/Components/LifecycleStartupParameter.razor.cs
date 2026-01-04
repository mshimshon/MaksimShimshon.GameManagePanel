using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components;

public partial class LifecycleStartupParameter : ComponentBase
{
    [Inject] public ISwizzleFactory SwizzleFactory { get; set; } = default!;

    private LifecycleStartupParameterViewModel _viewModel = default!;
    protected override async Task OnInitializedAsync()
    {
        // Create or Get Exisintg Hook Binding
        var articleVMHook = SwizzleFactory.CreateOrGet<LifecycleStartupParameterViewModel>(() => this, ShouldUpdate);
        // Get View Model Type Instance of the Hook
        _viewModel = articleVMHook.GetViewModel<LifecycleStartupParameterViewModel>()!;
    }
    private Task ShouldUpdate() => InvokeAsync(StateHasChanged);
}
