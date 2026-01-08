using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;

public class LifecycleStartupParameterFieldViewModel : WidgetViewModelBase, ILifecycleStartupParameterFieldViewModel
{



    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    private LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, UpdateChanges);

    public GameStartupParameterEntity Parameter { get; set; } = default!;

    public string Value { get; set; } = string.Empty;
    public string InitialValue { get; set; } = string.Empty;
    public bool HasValidation => Parameter.Validation != default;
    public bool IsList => Parameter.Validation?.AllowedValues != default &&
        Parameter.Key.StartupParameterType == StartupParameterType.List;
    public bool IsDecimal => Parameter.Key.StartupParameterType == StartupParameterType.Decimal;
    public bool IsInt => Parameter.Key.StartupParameterType == StartupParameterType.Int;
    public bool IsNumber => IsDecimal || IsInt;



    public LifecycleStartupParameterFieldViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

    public async Task Save()
    {
        await _dispatcher.Prepare<LifecycleUpdateStartupParameterAction>()
            .With(p => p.Key, Parameter.Key.Key)
            .With(p => p.Value, Value)
            .DispatchAsync();
    }

    public void Reset()
    {
        Value = InitialValue;
        _ = UpdateChanges();
    }
    public bool Validate()
    {

        return true;
    }
}
