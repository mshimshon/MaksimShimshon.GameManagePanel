using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

public record UpdateStartupParameterAction : ISafeAction
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
