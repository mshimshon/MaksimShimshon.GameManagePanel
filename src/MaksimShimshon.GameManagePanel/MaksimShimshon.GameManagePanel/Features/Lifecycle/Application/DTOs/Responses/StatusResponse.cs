namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record StatusResponse
{
    public string Status { get; set; } = default!;
    public StatusServerResponse? Server { get; set; }
    public GameInfoResponse GameInfo { get; set; } = default!;
    public string? ConfigFile { get; set; }
    public StatusResourcesResponse? Resources { get; set; }
    public DateTime Timestamp { get; set; }
}