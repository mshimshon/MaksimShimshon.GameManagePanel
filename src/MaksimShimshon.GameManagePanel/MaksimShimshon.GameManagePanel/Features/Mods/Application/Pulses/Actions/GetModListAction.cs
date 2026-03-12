using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;

public sealed record GetModListAction : IAction
{
    public Guid Id { get; set; }
}
