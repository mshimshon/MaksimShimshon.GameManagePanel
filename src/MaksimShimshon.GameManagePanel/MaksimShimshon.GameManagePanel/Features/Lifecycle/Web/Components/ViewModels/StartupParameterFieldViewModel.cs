using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

public class StartupParameterFieldViewModel : WidgetViewModelBase, IStartupParameterFieldViewModel
{



    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    private GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, UpdateChanges);
    public bool IsTouched => InitialValue != Value;

    public GameStartupParameterEntity Parameter { get; set; } = default!;

    public string Value { get; set; } = string.Empty;
    public string InitialValue { get; set; } = string.Empty;
    public bool HasValidation => Parameter.Validation != default;
    public bool IsList => Parameter.Validation?.AllowedValues != default &&
        Parameter.Key.StartupParameterType == StartupParameterType.List;
    public bool IsDecimal => Parameter.Key.StartupParameterType == StartupParameterType.Decimal;
    public bool IsInt => Parameter.Key.StartupParameterType == StartupParameterType.Int;
    public bool IsNumber => IsDecimal || IsInt;



    public StartupParameterFieldViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

    public async Task Save()
    {
        await _dispatcher.Prepare<UpdateStartupParameterAction>()
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

    public string GetLabel()
    {
        string defaultValue = InitialValue;
        if (string.IsNullOrWhiteSpace(defaultValue))
            return Parameter.Key.Key;
        return $"{Parameter.Key.Key} (Current: {defaultValue})";
    }
}
