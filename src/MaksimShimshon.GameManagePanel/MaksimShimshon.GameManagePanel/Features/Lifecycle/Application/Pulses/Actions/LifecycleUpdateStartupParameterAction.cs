using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

public record LifecycleUpdateStartupParameterAction : ISafeAction
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
