using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

public record ModListLocalState : IStateFeature
{
    public ModListEntity? Current { get; init; }
    public bool IsCurrentLoading { get; init; }

    public bool IsCreationLoading { get; init; }
    public string? HasCreatingErrors { get; init; }

}
