using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

public sealed record GameInfoEntity
{
    public string Name { get; init; } = default!;
    public bool Steam => SteamInfo != default;
    public SteamGameId? SteamInfo { get; init; }
    public bool Modding { get; init; }
    public bool ManualModUpload { get; init; }
    public ICollection<GameStartupParameterEntity>? StartupParameters { get; init; }
}
