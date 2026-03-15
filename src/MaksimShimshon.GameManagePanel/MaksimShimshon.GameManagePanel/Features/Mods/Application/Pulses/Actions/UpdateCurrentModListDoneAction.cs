using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;

internal sealed record UpdateCurrentModListDoneAction : IAction
{
    public ModListDescriptor? Current { get; set; }
}