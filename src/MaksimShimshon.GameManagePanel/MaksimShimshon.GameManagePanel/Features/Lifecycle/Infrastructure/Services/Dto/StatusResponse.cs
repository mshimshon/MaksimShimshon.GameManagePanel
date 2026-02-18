using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record StatusResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;

    [JsonPropertyName("server")]
    public StatusServerResponse? Server { get; set; }

    [JsonPropertyName("game_info")]
    public GameInfoResponse GameInfo { get; set; } = default!;

    [JsonPropertyName("config_file")]
    public string? ConfigFile { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}