using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;

public class LifecycleStartupParameterFieldViewModel
{
    private bool _loading = false;
    public bool Loading
    {
        get => _loading;
        private set
        {
            bool hasChanged = value != _loading;
            _loading = value;
            if (hasChanged)
                _ = SpreadChanges?.Invoke();
        }
    }

    public event Func<Task>? SpreadChanges;
    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();

    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, OnUpdate);

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
        Console.WriteLine($"{Value} = {InitialValue}");
        Value = InitialValue;
        _ = SpreadChanges?.Invoke();
    }
    public bool Validate()
    {

        return true;
    }
}
