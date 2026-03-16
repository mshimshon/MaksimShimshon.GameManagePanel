namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto;

public sealed class ModResponse
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public List<ModResponse>? Dependencies { get; set; }
}
