using LunaticPanel.Core;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components;

public partial class LifecycleStartupParameterField : ComponentBase, IDisposable
{

    private readonly IPluginService<PluginEntry> _pluginService;
    public LifecycleStartupParameterField(IPluginService<PluginEntry> pluginService)
    {
        _pluginService = pluginService;
    }

    [Parameter]
    public GameStartupParameterEntity GameStartupParameter { get; set; } = default!;
    [Parameter]
    public string InitialValue { get; set; } = default!;
    [Inject] public LifecycleStartupParameterFieldViewModel ViewModel { get; set; } = default!;

    public MudSelect<string>? MudSelectRef { get; set; }
    public MudNumericField<int>? MudNumericFieldIntRef { get; set; }
    public MudNumericField<double>? MudNumericFieldDecimalRef { get; set; }
    public MudTextField<string>? MudTextFieldRef { get; set; }
    public MudSwitch<bool>? MudSwitchRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ViewModel = _pluginService.GetRequired<LifecycleStartupParameterFieldViewModel>();
        ViewModel.Parameter = GameStartupParameter;
        ViewModel.InitialValue = InitialValue;
        ViewModel.Value = InitialValue;
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
    private int GetMaxLength() => ViewModel.Parameter.Validation?.MaxLength ?? 524288;


    private bool IsTouched => ViewModel.InitialValue != ViewModel.Value;
    private int ValueInt
    {
        get => !string.IsNullOrWhiteSpace(ViewModel.Value) ? int.Parse(ViewModel.Value) : 0;
        set { ViewModel.Value = value.ToString(); }
    }

    private double ValueDecimal
    {
        get => !string.IsNullOrWhiteSpace(ViewModel.Value) ? double.Parse(ViewModel.Value) : 0;
        set { ViewModel.Value = value.ToString(); }
    }

    private bool ValueBool
    {
        get => !string.IsNullOrWhiteSpace(ViewModel.Value) && bool.Parse(ViewModel.Value);
        set { ViewModel.Value = value.ToString(); }
    }
}
