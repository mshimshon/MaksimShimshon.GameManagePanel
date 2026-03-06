using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;

internal sealed record UpdateCurrentModListDoneAction : IAction
{
    public ModListEntity? Current { get; set; }
}