using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;

public sealed record CreateModListDoneAction : IAction
{
    public bool Failed { get; set; }
}
