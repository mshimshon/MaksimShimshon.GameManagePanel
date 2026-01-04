namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record GameInfoResponse
{
    public string Name { get; set; } = default!;
    public bool Steam { get; set; }
    public string? SteamAppId { get; set; }
    public bool Modding { get; set; }
    public bool Workshop { get; set; }
    public bool ManualModUpload { get; set; }
    public List<GameStartupParameterResponse> Parameters { get; init; } = new();

}
