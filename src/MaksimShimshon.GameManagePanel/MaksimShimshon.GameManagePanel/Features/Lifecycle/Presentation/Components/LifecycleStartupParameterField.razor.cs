using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components;

public partial class LifecycleStartupParameterField : ComponentBase
{
    [Parameter]
    public GameStartupParameterEntity GameStartupParameter { get; set; } = default!;
    [Parameter]
    public string InitialValue { get; set; } = default!;
    [Inject] public ISwizzleFactory SwizzleFactory { get; set; } = default!;

    public MudSelect<string>? MudSelectRef { get; set; }
    public MudNumericField<int>? MudNumericFieldIntRef { get; set; }
    public MudNumericField<double>? MudNumericFieldDecimalRef { get; set; }
    public MudTextField<string>? MudTextFieldRef { get; set; }
    public MudSwitch<bool>? MudSwitchRef { get; set; }

    private LifecycleStartupParameterFieldViewModel _viewModel = default!;
    protected override async Task OnInitializedAsync()
    {
        // Create or Get Exisintg Hook Binding
        var articleVMHook = SwizzleFactory.
            CreateOrGet<LifecycleStartupParameterFieldViewModel>(() => this, ShouldUpdate);
        // Get View Model Type Instance of the Hook
        _viewModel = articleVMHook.GetViewModel<LifecycleStartupParameterFieldViewModel>()!;

        _viewModel.Parameter = GameStartupParameter;
        _viewModel.InitialValue = InitialValue;
        _viewModel.Value = InitialValue;
        Console.WriteLine($"InitialValue = {_viewModel.InitialValue}");
        Console.WriteLine($"Value = {_viewModel.Value}");

    }
    private Task ShouldUpdate() => InvokeAsync(StateHasChanged);

    private int GetMaxLength() => _viewModel.Parameter.Validation?.MaxLength ?? 524288;


    private bool IsTouched => _viewModel.InitialValue != _viewModel.Value;
    private int ValueInt
    {
        get
        {
            Console.WriteLine(_viewModel.Value);
            return !string.IsNullOrWhiteSpace(_viewModel.Value) ? int.Parse(_viewModel.Value) : 0;
        }
        set { _viewModel.Value = value.ToString(); }
    }

    private double ValueDecimal
    {
        get
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Value) ? double.Parse(_viewModel.Value) : 0;
        }
        set { _viewModel.Value = value.ToString(); }
    }

    private bool ValueBool
    {
        get
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Value) ? bool.Parse(_viewModel.Value) : false;
        }
        set { _viewModel.Value = value.ToString(); }
    }
}
