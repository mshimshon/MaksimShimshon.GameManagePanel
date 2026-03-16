namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto;

public sealed class ModListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Dictionary<string, List<ModResponse>> Mods { get; set; } = new();
}
