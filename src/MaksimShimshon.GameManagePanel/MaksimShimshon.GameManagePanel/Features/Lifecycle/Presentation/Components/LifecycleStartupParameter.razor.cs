using LunaticPanel.Core;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components;

public partial class LifecycleStartupParameter : ComponentBase, IDisposable
{
    private readonly IPluginService<PluginEntry> _pluginService;
    public LifecycleStartupParameter(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }
    [Inject] public LifecycleStartupParameterViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel = _pluginService.GetRequired<LifecycleStartupParameterViewModel>();
        ViewModel.SpreadChanges += ShouldUpdate;
    }

    private Task ShouldUpdate() => InvokeAsync(StateHasChanged);
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        ViewModel.SpreadChanges -= ShouldUpdate;
    }

    private string GetInitialValue(GameStartupParameterEntity parameter) =>
        ViewModel.GameInfoState.StartupParameters.ContainsKey(parameter.Key.Key) ?
        ViewModel.GameInfoState.StartupParameters[parameter.Key.Key] :
        parameter.DefaultValue ?? string.Empty;
}
