using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record FetchStartupParametersDoneAction : IAction
{
    public Dictionary<string, string> StartupParameters { get; set; } = default!;
}
